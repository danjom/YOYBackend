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
using YOY.ValidationAPI.APIKeyAuth.Models.v1.PurchaseDispatch.POCO;
using YOY.Values;

namespace YOY.ValidationAPI.APIKeyAuth.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DispatchPurchaseController : ControllerBase
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

        private const int dispatchValidationRequestValidMins = 10;

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
        [HttpPost]
        public IActionResult Post([FromBody] PurchaseDispatchRequest model)
        {
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));
            IActionResult result;

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;
                result = new BadRequestObjectResult(new { message = errorMsg });

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
                    bool success = true;
                    bool valid = false;

                    if (iOTDevice != null && iOTDevice.TenantId != null)
                    {
                        Purchase purchase = this._businessObjects.Purchases.Get(iOTDevice.TenantId, model.PurchaseCode, PurchaseCodeTypes.NumericOnly, PurchaseStatuses.DispatchValidationRequested);

                        if (purchase != null)
                        {
                            if (purchase.Status != PurchaseStatuses.Delivered)//In this case the order has been delivered
                            {
                                List<PurchaseItem> purchaseItems = this._businessObjects.Purchases.Gets(purchase.Id, PurchaseItemRelatedReferences.PurchaseId);

                                if(purchaseItems?.Count > 0)
                                {
                                    foreach(PurchaseItem item in purchaseItems)
                                    {
                                        if (item.Status >= PurchaseItemStatuses.Delivered)//Remove all the items that can't be delivered
                                            purchaseItems.Remove(item);

                                        if (valid)
                                        {
                                            if (item.Status < PurchaseItemStatuses.Delivered)
                                            {
                                                if (item.Status == PurchaseItemStatuses.DispatchValidationRequested && (DateTime.UtcNow - item.UpdatedDate).TotalMinutes > dispatchValidationRequestValidMins)
                                                {
                                                    success = this._businessObjects.Purchases.Put(item.Id, PurchaseItemStatuses.Payed);

                                                    valid = success;

                                                    if (success)
                                                        item.Status = PurchaseItemStatuses.Payed;
                                                }

                                                if (valid && item.Status == PurchaseItemStatuses.Payed)
                                                {
                                                    success = this._businessObjects.Purchases.Put(item.Id, PurchaseItemStatuses.DispatchValidationRequested);

                                                    valid = success;

                                                }
                                            }
                                        }
                                        
                                    }

                                    if(valid && purchaseItems?.Count > 0)
                                    {
                                        this._businessObjects.Purchases.Put(purchase.Id, (Guid)iOTDevice.BranchId, iOTDevice.Id, PurchaseDispatchValidatorSourceTypes.iOTDevice, PurchaseStatuses.DispatchValidationRequested);

                                        DispatchValidationPurchase dispatchValidation = new DispatchValidationPurchase
                                        {
                                            DispatchCode = purchase.PurchaseCode,
                                            Message = _localizer["AskUserToConfirmDispatch", purchaseItems.Count].Value,
                                        };

                                        result = Ok(dispatchValidation);
                                    }
                                    else
                                    {
                                        result = new BadRequestObjectResult(
                                                new ErrorResponse
                                                {
                                                    StatusCode = Values.StatusCodes.BadRequest,
                                                    DisplayMsgToUser = false,
                                                    DevError = _localizer["PurchaseCantBeDispatched"].Value,
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
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["InvalidPurchaseCode"].Value,
                                        MsgContent = _localizer["InvalidPurchaseCode"].Value,
                                        MsgTitle = "ERROR"
                                    });
                                }
                            }
                            else
                            {
                                result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["OrderAlreadyDelivered"].Value,
                                        MsgContent = _localizer["OrderAlreadyDelivered"].Value,
                                        MsgTitle = "ERROR"
                                    });
                            }
                        }
                        else
                        {
                            result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["InvalidPurchaseCode"].Value,
                                        MsgContent = _localizer["InvalidPurchaseCode"].Value,
                                        MsgTitle = "ERROR"
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
                                    DevError = _localizer["InvalidPayload"].Value,
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
        public DispatchPurchaseController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
