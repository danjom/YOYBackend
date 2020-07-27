using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.TextMessaging.Twilio;
using YOY.ThirdpartyServices.Services.Communication.SMS.TwilioSMS;
using YOY.UserAPI.Models.v1.IdentityModel;
using YOY.UserAPI.Models.v1.AccountValidation.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.Values;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AccountValidatorController : ControllerBase
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
        private UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        private readonly string ValidationMark = "[YOY Code]";
        private readonly string SMSSenderNumber = "+16783743384";
        private readonly string WhatsappSenderNumber = "+14155238886";
        private readonly int MinsToExpire = 10;

        private const int minPhoneNumberDaysLock = 120;

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

        /// <summary>
        /// Sends the validation code to a given phone number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("post")]
        [HttpPost]
        public IActionResult Post([FromBody] AccountPhoneNumberValidationRequest model)
        {
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));
            IActionResult result;

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;
                result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = _localizer["InvalidMobilePhone"].Value,
                                    MsgTitle = _localizer["InvalidMobilePhoneTitle"].Value
                                });

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);

            }
            else
            {
                try
                {
                    UserData user = this._businessObjects.Users.Get(model.UserId, UserKeys.UserId);

                    if(user.CountryPhonePrefix == model.CountryPhonePrefix && user.PhoneNumber == model.PhoneNumber && user.PhoneNumberConfirmed)
                    {
                        result = new ConflictObjectResult(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.Conflict,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["UnchangedPhoneNumber"].Value,
                                        MsgContent = _localizer["UnchangedPhoneNumber"].Value,
                                        MsgTitle = _localizer["UnchangedPhoneNumberTitle"].Value,

                                    });
                    }
                    else
                    {
                        string latestUserId = "";
                        bool allowedPhone = false;

                        //bool phoneUniqueness = this._businessObjects.Users.CheckPhoneNumberUniqueness(model.PhoneNumber, model.CountryPhonePrefix);

                        DateTime? latestUsageDate = this._businessObjects.Users.Get(model.CountryPhonePrefix + " " + model.PhoneNumber, UserIdentityValueTypes.PhoneNumber, ref latestUserId);

                        if (latestUsageDate == null)
                        {
                            allowedPhone = true;

                        }
                        else
                        {

                            if (latestUserId != null && (((DateTime.UtcNow - (DateTime)latestUsageDate).TotalDays > minPhoneNumberDaysLock) || latestUserId == model.UserId))
                            {
                                allowedPhone = true;
                            }
                        }

                        if (allowedPhone)
                        {
                            Random generator = new Random();
                            string verificationCode = generator.Next(0, 99999).ToString("D5");

                            string validationSMS = _localizer["ValidationMsg", ValidationMark + verificationCode, MinsToExpire].Value;
                            string senderPhoneNumber = "";

                            switch (model.ChannelType)
                            {
                                case TextMessageChannels.SMS:
                                    senderPhoneNumber = SMSSenderNumber;
                                    break;
                                case TextMessageChannels.Whatsapp:
                                    senderPhoneNumber = WhatsappSenderNumber;
                                    break;
                            }

                            MessageResourceContent resourceContent = TwilioMessageHandler.SendPlainTextMessage(model.ChannelType, validationSMS, senderPhoneNumber, model.CountryPhonePrefix + model.PhoneNumber.Trim(), 180);

                            if (resourceContent != null)
                            {
                                this._businessObjects.TextMsgLogs.Post(model.UserId, new Guid(MembershipConfigValues.BaseCommerceId), TextMessageLogReferenceTypes.None, null, senderPhoneNumber, model.CountryPhonePrefix + model.PhoneNumber,
                                    validationSMS, "", "", verificationCode, TextMessageLogPurposeTypes.AccountValidation, model.ChannelType, TextMessageLogStatuses.SentRequested, TextMessageGateways.Twilio, resourceContent.SId,
                                    resourceContent.NumMedia, resourceContent.Direction, "OK", null, "", resourceContent.Uri, resourceContent.CreatedDate ?? DateTime.UtcNow, DateTime.UtcNow.AddMinutes(MinsToExpire));

                                result = Ok(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.Ok,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = false,
                                        DevError = "",
                                        MsgContent = "",
                                        MsgTitle = "",
                                    });
                            }
                            else
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                        }
                        else
                        {
                            result = new ConflictObjectResult(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.Conflict,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["PhoneNumberAlreadyRegistered"].Value,
                                        MsgContent = _localizer["RepeatedPhoneNumber"].Value,
                                        MsgTitle = _localizer["RepeatedPhoneNumberTitle"].Value,

                                    });
                        }
                    }

                }
                catch (Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.InnerException != null ? e.InnerException.Message : e.Message;

                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        /// Validates phone number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> Put([FromBody] UserInputtedValidationCode model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;
                result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
            }
            else
            {
                try
                {
                    TextMessageLog smsLog = this._businessObjects.TextMsgLogs.Get(model.UserId, model.CountryPhonePrefix + model.PhoneNumber, ValidationMark, model.ValidationCode, TextMessageLogPurposeTypes.AccountValidation, DateTime.UtcNow);

                    if(smsLog != null)
                    {
                        //Check if the phone needs to be unlinked from any account
                        string latestUserId = "";
                        bool allowedPhone = false;

                        //bool phoneUniqueness = this._businessObjects.Users.CheckPhoneNumberUniqueness(model.PhoneNumber, model.CountryPhonePrefix);

                        DateTime? latestUsageDate = this._businessObjects.Users.Get(model.CountryPhonePrefix + " " + model.PhoneNumber, UserIdentityValueTypes.PhoneNumber, ref latestUserId);

                        if (latestUsageDate == null)
                        {
                            allowedPhone = true;

                        }
                        else
                        {

                            if (latestUserId != null && (((DateTime.UtcNow - (DateTime)latestUsageDate).TotalDays > minPhoneNumberDaysLock) || latestUserId == model.UserId))
                            {
                                allowedPhone = true;
                            }
                        }

                        if (allowedPhone)
                        {

                            //Code is valid, phone needs to be set and marked as confirmed
                            AppUser u = await _userManager.FindByIdAsync(model.UserId);

                            u.CountryPhonePrefix = model.CountryPhonePrefix;
                            u.PhoneNumber = model.PhoneNumber;
                            u.PhoneNumberConfirmed = true;
                            IdentityResult updateUserResult = await _userManager.UpdateAsync(u);

                            this._businessObjects.TextMsgLogs.Put(smsLog.Id, TextMessageLogStatuses.ReadByUser);

                            if (updateUserResult.Succeeded)
                            {

                                //Registers the vinculation log
                                this._businessObjects.Users.Post(model.CountryPhonePrefix + " " + model.PhoneNumber, UserIdentityValueTypes.PhoneNumber, model.UserId);

                                if (!string.IsNullOrWhiteSpace(latestUserId))
                                {
                                    AppUser lastOwner = await _userManager.FindByIdAsync(latestUserId);

                                    if (lastOwner != null)
                                    {
                                        lastOwner.PhoneNumber = "";
                                        lastOwner.PhoneNumberConfirmed = false;
                                        updateUserResult = await _userManager.UpdateAsync(lastOwner);

                                        errorMsg = "Phone number: " + model.CountryPhonePrefix + " " + model.PhoneNumber + " unlink from userId " + latestUserId + " failed" ;

                                        if (!updateUserResult.Succeeded)
                                        {
                                            //Registers the invalid call
                                            this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                                StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                        }
                                    }
                                }

                                result = Ok(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.Ok,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = false,
                                        DevError = "",
                                        MsgContent = "",
                                        MsgTitle = "",
                                    });
                            }
                            else
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }

                    }
                    else
                    {
                        result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidValidationCode"].Value,
                                    MsgContent = _localizer["WrongValidationCode"].Value,
                                    MsgTitle = _localizer["WrongValidationCodeTitle"].Value,
                                });
                    }
                }
                catch(Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.InnerException != null ? e.InnerException.Message : e.Message;

                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }

        #endregion

        #region CONSTRUCTORS
        public AccountValidatorController(UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer)
        {
            this._userManager = userManager;
            this._localizer = localizer;
        }
        #endregion
    }
}
