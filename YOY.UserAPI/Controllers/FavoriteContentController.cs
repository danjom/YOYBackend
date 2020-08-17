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
using YOY.UserAPI.Models.v1.FavoriteContent.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteContentController : ControllerBase
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
        private readonly IStringLocalizer<SharedResources> _localizer;

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

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] FavedContentCore model)
        {
            IActionResult result;
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid)
                {
                    SavedItem item =   this._businessObjects.SavedItems.Post(model.ContentId, model.ContentType, model.CommerceId, null, model.UserId);
               
                    if(item != null)
                    {
                        FavedContentSuccessfulOperation successOperation = new FavedContentSuccessfulOperation
                        {
                            OperationType = ContainingActionTypes.Add,
                            CommerceId = model.CommerceId,
                            ContentId = model.ContentId,
                            ContentType = model.ContentType,
                            Message = _localizer["FavedContentAddedSuccessfully"]
                        };

                        result = Ok(successOperation);
                    }
                    else
                    {
                        errorMsg = "Saved item add failed";
                        result = new BadRequestObjectResult((
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["OperationFailed"].Value,
                                        MsgContent = _localizer["OperationFailedMsg"],
                                        MsgTitle = ""
                                    }));

                        //Registers the invalid call
                        this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                            Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                    }
                
                }
                else
                {
                    errorMsg = "Invalid payload";
                    result = new BadRequestObjectResult((
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = _localizer["OperationFailedMsg"],
                                    MsgTitle = ""
                                }));

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }

            }
            catch(Exception e)
            {
                errorMsg = "Error: An exception has occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }

            return result;
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete([FromBody] FavedContentCore model)
        {
            IActionResult result;
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid)
                {
                    bool success = this._businessObjects.SavedItems.Delete(model.ContentId, model.ContentType, model.UserId);

                    if (success)
                    {
                        FavedContentSuccessfulOperation successOperation = new FavedContentSuccessfulOperation
                        {
                            OperationType = ContainingActionTypes.Remove,
                            CommerceId = model.CommerceId,
                            ContentId = model.ContentId,
                            ContentType = model.ContentType,
                            Message = _localizer["FavedContentDeletedSuccessfully"]
                        };

                        result = Ok(successOperation);
                    }
                    else
                    {
                        errorMsg = "Saved item delete failed";
                        result = new BadRequestObjectResult((
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["OperationFailed"].Value,
                                        MsgContent = _localizer["OperationFailedMsg"],
                                        MsgTitle = ""
                                    }));

                        //Registers the invalid call
                        this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                            Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                    }

                }
                else
                {
                    errorMsg = "Invalid payload";
                    result = new BadRequestObjectResult((
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = _localizer["OperationFailedMsg"],
                                    MsgTitle = ""
                                }));

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }

            }
            catch (Exception e)
            {
                errorMsg = "Error: An exception has occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }

            return result;
        }

        #endregion

        #region CONSTRUCTORS
        public FavoriteContentController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
