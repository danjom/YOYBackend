using Twilio.TwiML;
using Twilio.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Twilio.TwiML.Messaging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using static System.Linq.Enumerable;
using System.Collections.Generic;
using YOY.DTO.Entities.Misc.TextMessaging.Twilio;
using YOY.ThirdpartyServices.Services.Communication.SMS.TwilioSMS;
using YOY.DAO.Entities;
using System;
using YOY.Values;
using YOY.DTO.Entities;
using YOY.ValidationAPI.TwilioWebhook.Logic.RandomGenerator;

namespace YOY.ValidationAPI.TwilioWebhook.Controllers
{


    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class WAValid8Controller : TwilioController
    {
        #region PROPERTIES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant;
        private BusinessObjects _businessObjects;

        private const int maxMinutesTolerance = 5;
        private const string whatsappIndicator = "whatsapp:";

        private const int controllerVersion = 1;
        #endregion

        #region METHODS

        /// <summary>
        /// Initializes the business objects
        /// </summary>
        /// <param name="commerceId"></param>
        private void Initialize(Guid commerceId)
        {

            if (_tenant == null || _tenant.TenantId != commerceId)
            {
                _tenant = Tenant.GetInstance(commerceId);
            }

            if (_businessObjects == null)
            {
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }
        }

        [Route("post")]
        [HttpPostAttribute]
        public TwiMLResult Post(IFormCollection formCollection)
        {
            int callId = 1;
            string parameters = "";

            try
            {
                Guid tenantId = new Guid(MembershipConfigValues.BaseCommerceId);
                Initialize(tenantId);

                //Extract data
                string sId = formCollection["SmsSid"]; // "SMdb87f8f056c2e22b5301b79046cbc622";// 
                string status = formCollection["SmsStatus"]; //"received";// 
                string msg =  formCollection["Body"]; //"1"; //
                int numMedia = int.Parse(formCollection["NumMedia"]); // 0;// 
                //int numSegments = int.Parse(formCollection["NumSegments"]); // 0;// 
                string from = formCollection["From"]; //"whatsapp:+5212227216654";// 
                string to = formCollection["To"];// "whatsapp:+14155238886";// 
                string locationData = ""; 
                int channel = TextMessageChannels.SMS;

                if(!string.IsNullOrWhiteSpace(formCollection["Latitude"]) && !string.IsNullOrWhiteSpace(formCollection["Longitude"]))
                {
                    locationData = "Lat: " + formCollection["Latitude"] + " - Longitude: " + formCollection["Longitude"] + " - Address: " + formCollection["Address"];
                }

                parameters = "SId: " + sId + " - Status: " + status + " - Msg: " + msg + " - NumMedia: " + numMedia + " - From: " + from + " - To: " + to;

                if (to.Contains(whatsappIndicator))
                {
                   to = to.Replace(whatsappIndicator, "");
                   from = from.Replace(whatsappIndicator, "");

                    channel = TextMessageChannels.Whatsapp;
                }

                //Creates the received log 
                TextMessageLog messageLog = this._businessObjects.TextMsgLogs.Post("", tenantId, TextMessageLogReferenceTypes.None, null, from, to, msg, locationData, "", "", TextMessageLogPurposeTypes.Discover, channel, 
                    TextMessageLogStatuses.SuccessfullyDelivered, TextMessageGateways.Twilio, sId, numMedia, "inbound", "delivered", null, "", "", DateTime.UtcNow, null);

                //Now will check if the message was sent by a commerce employee
                Employee employee = this._businessObjects.Employees.Get(from, "Operator", 1);

                var response = new MessagingResponse();
                string responseMsg;
                Message message;
                Guid? newStepFlowId = null;
                bool valid = true;
                bool startNewFlow = false;

                if (employee != null)
                {

                    //Now needs to check if there is an operation flow in progress for this user
                    List<OperationFlowStepLog> stepLog = this._businessObjects.OperationFlowSteps.Gets(employee.Id.ToString(), OperationFlowOwners.Employee, from, DateTime.UtcNow.AddMinutes(-1*maxMinutesTolerance));

                    if(stepLog?.Count > 0)
                    {

                        if ((!stepLog[0].FlowCompleter))//If the most recent step isn't the completer of a given flow
                        {
                            if (stepLog.Count == 1)//This is the step where user needs to choose the action to operate
                            {
                                if (stepLog[0].Step == 1)//If user only received step one previously
                                {
                                    msg = msg.Trim();

                                    if (msg.Length == 1 && Char.IsDigit(msg[0]))
                                    {
                                        switch ((char)msg[0])
                                        {
                                            case (char)BusinessOperationFlags.GeneratePaymentRequestFromMessagging:

                                                //Add the step to the flow
                                                newStepFlowId = this._businessObjects.OperationFlowSteps.Post(employee.TenantId, employee.BranchId, employee.Id.ToString(), OperationFlowOwners.Employee, OperationFlowTypes.GeneratePaymentRequest,
                                                                 stepLog[0].Id, stepLog[0].OperationFlowCode, from, null, OperationFlowReferenceTypes.None, messageLog.Id, OperationFlowSourceTypes.Messaging, stepLog[0].Step + 1, true, true, false);

                                                if (newStepFlowId != null)
                                                {
                                                    this._businessObjects.OperationFlowSteps.Put(OperationFlowTypes.GeneratePaymentRequest, stepLog[0].Id);

                                                    //Needs to request the payment amount 
                                                    responseMsg = " 💳 *Procesar pago*: Ingrese el monto en *" + employee.CurrencyTypeName + "*";

                                                    message = new Message();
                                                    message.Body(responseMsg);

                                                    //message.Media(GOOD_BOY_URL);

                                                    response.Append(message);
                                                }


                                                break;
                                            case (char)BusinessOperationFlags.SetPurchaseAsDispatchedFromMessagging:

                                                //Add the step to the flow
                                                newStepFlowId = this._businessObjects.OperationFlowSteps.Post(employee.TenantId, employee.BranchId, employee.Id.ToString(), OperationFlowOwners.Employee, OperationFlowTypes.DispatchPurchase,
                                                                stepLog[0].Id, stepLog[0].OperationFlowCode, from, null, OperationFlowReferenceTypes.None, messageLog.Id, OperationFlowSourceTypes.Messaging, stepLog[0].Step + 1, true, true, false);

                                                if (newStepFlowId != null)
                                                {

                                                    this._businessObjects.OperationFlowSteps.Put(OperationFlowTypes.DispatchPurchase, stepLog[0].Id);

                                                    //Needs to ask for the purchase code or qr picture

                                                    responseMsg = " 🛒 *Entregar compra*: _Solicite al comprador el código de compra_.\n\n*Ingréselo* aquí ✍🏼 ó *envíe una fotografía* del código QR 📱";

                                                    message = new Message();
                                                    message.Body(responseMsg);

                                                    response.Append(message);
                                                }

                                                break;
                                            default:
                                                valid = false;
                                                break;

                                        }
                                    }
                                    else
                                    {
                                        valid = false;
                                    }
                                }
                            }
                            else
                            {
                                if (stepLog.Count == 2 && !stepLog[0].FlowCompleter)//In total the procceses has only 3 steps
                                {
                                    //There is a flow in proccess

                                    if (stepLog[0].OperationType == stepLog[1].OperationType)
                                    {
                                        switch (stepLog[0].OperationType)
                                        {
                                            case OperationFlowTypes.GeneratePaymentRequest:

                                                decimal value;
                                                if (Decimal.TryParse(msg, out value))
                                                {

                                                    //Add the step to the flow
                                                    newStepFlowId = this._businessObjects.OperationFlowSteps.Post(employee.TenantId, employee.BranchId, employee.Id.ToString(), OperationFlowOwners.Employee, OperationFlowTypes.GeneratePaymentRequest,
                                                        stepLog[0].Id, stepLog[0].OperationFlowCode, from, null, OperationFlowReferenceTypes.Purchase, messageLog.Id, OperationFlowSourceTypes.Messaging, stepLog[0].Step + 1, true, true, true);

                                                    if (newStepFlowId != null)
                                                    {
                                                        //Needs to create the Payment request log
                                                        //PENDING

                                                        //Now need to set the purchase as reference for all the steps of the flow
                                                        this._businessObjects.OperationFlowSteps.Put(stepLog[0].OperationFlowCode, Guid.NewGuid()/*Here goes the payment request Id*/, OperationFlowReferenceTypes.PaymentRequest, stepLog[1].CreatedDate);

                                                        //Needs to generate the QR code
                                                        var mediaUrl = new Uri("https://res.cloudinary.com/yoyimgs/image/upload/v1594366995/dev/payqr/test-qr1.png");

                                                        msg = String.Format("{0:n}", value);

                                                        //Needs to request the payment amount 
                                                        responseMsg = " 💳 _Solicitud de pago por_ *" + employee.CurrencySymbol + msg + "*\nSolicite al usuario *escanear este código con YOY* para completar el pago";

                                                        message = new Message();
                                                        message.Body(responseMsg);
                                                        message.Media(mediaUrl);

                                                        response.Append(message);

                                                    }
                                                }


                                                break;
                                            case OperationFlowTypes.DispatchPurchase:

                                                //Add the step to the flow
                                                newStepFlowId = this._businessObjects.OperationFlowSteps.Post(employee.TenantId, employee.BranchId, employee.Id.ToString(), OperationFlowOwners.Employee, OperationFlowTypes.DispatchPurchase,
                                                                stepLog[0].Id, stepLog[0].OperationFlowCode, from, null, OperationFlowReferenceTypes.Purchase, messageLog.Id, OperationFlowSourceTypes.Messaging, stepLog[0].Step + 1, true, true, true);

                                                if (newStepFlowId != null)
                                                {

                                                    //Now needs to mark the purchase as delivered
                                                    //PENDING

                                                    //Now need to set the purchase as reference for all the steps of the flow
                                                    this._businessObjects.OperationFlowSteps.Put(stepLog[0].OperationFlowCode, Guid.NewGuid()/*Here goes the purchase Id*/, OperationFlowReferenceTypes.Purchase, stepLog[1].CreatedDate);

                                                    //Now informs about the purchase
                                                    responseMsg = " 🛒 *Comprador con cuenta*: 15482475, _confirme el número de cuenta antes de entregar_🔎.\n\n*Detalle de compra:\nZapatos Adidas XF45 -> CANTIDAD: 1\n2- Camiseta Nike Max Pro -> CANTIDAD 2";

                                                    message = new Message();
                                                    message.Body(responseMsg);

                                                    response.Append(message);
                                                }


                                                break;
                                        }
                                    }
                                    else
                                    {
                                        startNewFlow = true;

                                        this._businessObjects.OperationFlowSteps.Put(stepLog[0].Id, ChangeTypes.IsValidState);
                                        this._businessObjects.OperationFlowSteps.Put(stepLog[1].Id, ChangeTypes.IsValidState);
                                    }

                                }
                                else
                                {
                                    //Need to start a new flow since the last one has been completed
                                    startNewFlow = true;
                                }
                            }
                        }
                        else
                        {
                            startNewFlow = true;
                        }
                    }
                    else
                    {

                        //There is no flow in process, then needa to start a new one
                        startNewFlow = true;
                        
                    }

                    if (!valid)
                    {
                        responseMsg = "❌ ERROR, la opción ingresada *es inválida*, ingrese la operación que desea realizar: \n\n" + " *1*- Procesar Pago 💳\n*5*- Entregar compra 🛒";

                        message = new Message();
                        message.Body(responseMsg);

                        response.Append(message);
                    }
                    else
                    {
                        if (startNewFlow)
                        {
                            //Is the start of a new flow
                            newStepFlowId = this._businessObjects.OperationFlowSteps.Post(employee.TenantId, employee.BranchId, employee.Id.ToString(), OperationFlowOwners.Employee, OperationFlowTypes.NotDetermined,
                                             null, RandomStringGenerator.Generate(8, 0), from, null, OperationFlowReferenceTypes.None, messageLog.Id, OperationFlowSourceTypes.Messaging, 1, true, true, false);

                            if (newStepFlowId != null)
                            {
                                responseMsg = "¡Hola, *" + employee.Name + "*!, como colaborador de *" + employee.BranchName + "*, ingrese la operación que desea realizar: \n\n" + " *1*- Procesar Pago 💳\n\n*5*- Entregar compra 🛒";

                                message = new Message();
                                message.Body(responseMsg);
                                //message.Media(GOOD_BOY_URL);

                                response.Append(message);
                            }
                        }
                    }


                    return TwiML(response);
                }
                else
                {

                    message = new Message();
                    message.Body("Lo sentimos, no tienes permiso para utilizar este servicio, para recibir ayuda contacta con soporte@clubyoy.com");
                    //message.Media(GOOD_BOY_URL);

                    response.Append(message);

                    return TwiML(response);
                }


            }
            catch(Exception e)
            {
                var response = new MessagingResponse();

                var message = new Message();
                message.Body("Lo sentimos, ha ocurrido un error, intenta de nuevo");
                //message.Media(GOOD_BOY_URL);

                response.Append(message);

                string error = e.InnerException != null ? e.InnerException.ToString() : e.Message;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, error);

                return TwiML(response);
            }


        }


        [Route("get")]
        [HttpGetAttribute]
        public IActionResult Get()
        {

            return Ok("working");
        }

        #endregion
    }
}