using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Structure.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.UserAPI.Models.v1.ShoppingCart.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
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
        public IActionResult Post([FromBody] NewShoppingCartItem model)
        {
            IActionResult result;
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid && model.Quantity > 0)
                {
                    string preferences;
                    XElement chosenPreferences = null;
                    bool hasPreferences = false;

                    if (model.ChosenPreferences?.Count > 0)
                    {
                        hasPreferences = true;
                        preferences = "<Preferences>";

                        foreach (Pair<Guid, Guid> item in model.ChosenPreferences)
                        {
                            preferences += "<Element id:'" + item.Key + ":" + item.Value + "'/>";
                        }

                        preferences = "</Preferences>";


                        chosenPreferences = XElement.Parse(preferences);
                    }

                    ShoppingCartItem cartItem = this._businessObjects.ShoppingCartItems.Post(model.CommerceId, model.UserId, model.DealId, model.Quantity, hasPreferences, chosenPreferences);


                    if (cartItem != null)
                    {
                        ShoppingCartItemSuccessfulOperation successOperation = new ShoppingCartItemSuccessfulOperation
                        {
                            OperationType = ContainingActionTypes.Add,
                            CommerceId = cartItem.TenantId,
                            Id = cartItem.Id,
                            DealId = cartItem.OfferId,
                            Quantity = cartItem.Quantity,
                            Message = _localizer["CartItemAddedSuccessfully"]
                        };

                        result = Ok(successOperation);
                    }
                    else
                    {
                        errorMsg = "Shopping cart item add failed";
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

        [HttpPut]
        [Route("put")]
        public IActionResult Put([FromBody] UpdatedShoppingCartItem model)
        {
            IActionResult result;
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid && model.Quantity > 0)
                {
                    string preferences;
                    XElement chosenPreferences = null;

                    if (model.ChosenPreferences?.Count > 0)
                    {
                        preferences = "<Preferences>";

                        foreach (Pair<Guid, Guid> item in model.ChosenPreferences)
                        {
                            preferences += "<Element id:'" + item.Key + ":" + item.Value + "'/>";
                        }

                        preferences = "</Preferences>";


                        chosenPreferences = XElement.Parse(preferences);
                    }

                    ShoppingCartItem cartItem = this._businessObjects.ShoppingCartItems.Put(model.ItemId, model.Quantity, chosenPreferences);


                    if (cartItem != null)
                    {
                        ShoppingCartItemSuccessfulOperation successOperation = new ShoppingCartItemSuccessfulOperation
                        {
                            OperationType = ContainingActionTypes.Updated,
                            CommerceId = cartItem.TenantId,
                            Id = cartItem.Id,
                            DealId = cartItem.OfferId,
                            Quantity = cartItem.Quantity,
                            Message = _localizer["CartItemUpdatedSuccessfully"]
                        };

                        result = Ok(successOperation);
                    }
                    else
                    {
                        errorMsg = "Shopping cart item update failed";
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



        [HttpPut]
        [Route("putQuantity")]
        public IActionResult Put([FromBody] ShoppingCartItemUpdatedQuantity model)
        {
            IActionResult result;
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid && model.Quantity > 0)
                {

                    ShoppingCartItem cartItem = this._businessObjects.ShoppingCartItems.Put(model.ItemId, model.Quantity, null);


                    if (cartItem != null)
                    {
                        ShoppingCartItemSuccessfulOperation successOperation = new ShoppingCartItemSuccessfulOperation
                        {
                            OperationType = ContainingActionTypes.Updated,
                            CommerceId = cartItem.TenantId,
                            Id = cartItem.Id,
                            DealId = cartItem.OfferId,
                            Quantity = cartItem.Quantity,
                            Message = _localizer["CartItemUpdatedSuccessfully"]
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

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete([FromBody] DeletedShoppingCartItem model)
        {
            IActionResult result;
            int callId = 3;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid)
                {
                    ShoppingCartItem cartItem = this._businessObjects.ShoppingCartItems.Delete(model.ItemId);

                    if (cartItem != null)
                    {
                        ShoppingCartItemSuccessfulOperation successOperation;

                        if (model.ActionType == ShoppingCartDeleteActionTypes.PassToFavorites)
                        {
                            SavedItem savedItem = this._businessObjects.SavedItems.Get(SavedItemReferenceTypes.Offer, cartItem.OfferId);

                            if(savedItem == null)
                            {
                                //Needs to be added

                                savedItem = this._businessObjects.SavedItems.Post(cartItem.OfferId, SavedItemReferenceTypes.Offer, cartItem.TenantId, null, cartItem.UserId);
                            }

                            if(savedItem != null)
                            {
                                successOperation = new ShoppingCartItemSuccessfulOperation
                                {
                                    OperationType = ContainingActionTypes.TransferredSuccess,
                                    CommerceId = cartItem.TenantId,
                                    Id = cartItem.Id,
                                    DealId = cartItem.OfferId,
                                    Quantity = 0,
                                    Message = _localizer["CartItemMovedToFavoritesSuccessfully"]
                                };
                            }
                            else
                            {
                                successOperation = new ShoppingCartItemSuccessfulOperation
                                {
                                    OperationType = ContainingActionTypes.TransferredFail,
                                    CommerceId = cartItem.TenantId,
                                    Id = cartItem.Id,
                                    DealId = Guid.Empty,
                                    Quantity = 0,
                                    Message = _localizer["CartItemMovedToFavoritesFailed"]
                                };
                            }
                        }
                        else
                        {
                            successOperation = new ShoppingCartItemSuccessfulOperation
                            {
                                OperationType = ContainingActionTypes.Remove,
                                CommerceId = cartItem.TenantId,
                                Id = cartItem.Id,
                                Quantity = 0,
                                Message = _localizer["CartItemDeletedSuccessfully"]
                            };
                        }
                        

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
        public ShoppingCartController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion

    }
}
