using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.ValidationAPI.APIKeyAuth.Models.v1.Misc.BasicResponse.POCO;
using YOY.ValidationAPI.APIKeyAuth.Models.v1.PaymentRequest.POCO;
using YOY.Values;

namespace YOY.ValidationAPI.APIKeyAuth.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentRequestController : ControllerBase
    {
        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant;
        private BusinessObjects _businessObjects;
        private readonly IStringLocalizer<SharedResources> _localizer;

        private const int paymentRequestValidMins = 10;

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

        private string GetCurrencySymbol(int currencyType)
        {
            string currencyTypeName = currencyType switch
            {
                CurrencyTypes.CostaRicanColon => CurrencySymbols.CostaRicanColon,
                CurrencyTypes.USDollar => CurrencySymbols.USDollar,
                CurrencyTypes.GuatemalanQuetzal => CurrencySymbols.GuatemalanQuetzal,
                CurrencyTypes.HonduranLempira => CurrencySymbols.HonduranLempira,
                CurrencyTypes.NicaraguanCordoba => CurrencySymbols.NicaraguanCordoba,
                CurrencyTypes.MexicanPeso => CurrencySymbols.MexicanPeso,
                CurrencyTypes.ColombianPeso => CurrencySymbols.ColombianPeso,
                _ => "--",
            };
            return currencyTypeName;
        }

        [Route("get")]
        [HttpGet]
        public IActionResult Get(string deviceKey, string requestCode)
        {

            int callId = 1;
            string parameters = "DeviceKey: " + deviceKey + " - RequestCode: " + requestCode;
            string errorMsg;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));
            IActionResult result;

            if (string.IsNullOrWhiteSpace(deviceKey) || string.IsNullOrWhiteSpace(requestCode))
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(deviceKey, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
            }
            else
            {
                try
                {
                    HardwareIOTDevice iOTDevice = this._businessObjects.HardwareDevices.Get(deviceKey);

                    if(iOTDevice != null)
                    {
                        PaymentRequest paymentRequest = this._businessObjects.PaymentRequests.Get(requestCode, PaymentRequestsSourceTypes.iOTDevice, iOTDevice.Id, DateTime.Now);

                        if (paymentRequest != null)
                        {
                            PaymentRequestStatus requestStatus = new PaymentRequestStatus
                            {
                                RequestCode = paymentRequest.OpCode ?? requestCode,
                                Status = paymentRequest.Status,
                                Expired = paymentRequest.ExpiratinDate < DateTime.UtcNow,
                                PaymentCompleted = paymentRequest.Status == PaymentRequestStatuses.Completed
                            };

                            result = Ok(requestStatus);
                        }
                        else
                        {
                            result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidRequestCode"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                        }
                    }
                    else
                    {
                        result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidDeviceKey"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                    }
                }
                catch(Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(deviceKey, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            return result;
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post([FromBody] NewPaymentRequest model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));
            IActionResult result;

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.DeviceKey, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

            }
            else
            {

                try
                {
                    HardwareIOTDevice iOTDevice = this._businessObjects.HardwareDevices.Get(model.DeviceKey);

                    if (iOTDevice != null && iOTDevice.TenantId != null && (model.CurrencyType >= CurrencyTypes.CostaRicanColon && model.CurrencyType <= CurrencyTypes.ColombianPeso))
                    {
                        PaymentRequest request = this._businessObjects.PaymentRequests.Post((Guid)iOTDevice.TenantId, iOTDevice.BranchId, PaymentRequestsSourceTypes.iOTDevice, iOTDevice.Id, "", null, model.Amount, model.CurrencyType, this.GetCurrencySymbol(model.CurrencyType), DateTime.UtcNow.AddMinutes(paymentRequestValidMins));


                        if (request != null)
                        {
                            CreatedPaymentRequest createdPaymentRequest = new CreatedPaymentRequest
                            {
                                RequestCode = request.OpCode,
                                ExpirationDate = request.ExpiratinDate,
                                MessageToDisplay = _localizer["PaymentRequestCreated", request.CurrencySymbol + request.Amount].Value
                            };

                            result = Ok(createdPaymentRequest);
                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }

                    }
                    else
                    {
                        result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        DisplayMsgToUser = false,
                                        DevError = _localizer["InvalidBranchOrCurrency"].Value,
                                        MsgContent = "",
                                        MsgTitle = ""
                                    });
                    }
                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.DeviceKey, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
                
            }

            return result;
        }

        #endregion

        #region CONSTRUCTORS
        public PaymentRequestController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
