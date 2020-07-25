using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.UserAPI.Logic.Account;
using YOY.UserAPI.Models.v1.IdentityModel;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.UserAPI.Models.v1.ThirdpartyLogin.Apple.POCO;
using YOY.UserAPI.Models.v1.ThirdpartyLogin.User.POCO;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AppleAccountController : ControllerBase
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

        private async Task<UserIdentificationWithPassword> CreateUserFromApple([FromBody] AppleUserExtractedData model)
        {
            UserIdentificationWithPassword userIdentification;
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            if (!ModelState.IsValid)
            {
                userIdentification = null;
            }
            else
            {
                try
                {

                    AppUser user = new AppUser
                    {
                        UserName = model.Email,
                        Name = model.Name,
                        DateOfBirth = null,
                        Email = model.Email,
                        Gender = model.Gender,
                        AppleId = model.AppleId,
                        FBId = "",
                        GoogleId = "",
                        StateId = null,
                        AccountCode = model.Email.Length > 45 ? model.Email.Substring(0, 45) : model.Email,//Temporary
                        EmailConfirmed = true
                    };

                    IdentityResult addUserResult = await this._userManager.CreateAsync(user, model.Password);


                    if (addUserResult.Succeeded)
                    {
                        Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                        UserData userData = this._businessObjects.Users.Get(model.Email, UserKeys.Username);

                        //The membership is created to link user the default commerce
                        Membership customerMembership = this._businessObjects.Memberships.Post(userData.Id, UserKeys.UserId, new Guid(MembershipConfigValues.BaseCommerceId), MembershipCreationReasonTypes.OpenWallet, (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue), MembershipPointsOperationObjectiveTypes.YOYWallet);

                        if (customerMembership != null)
                        {
                            //Needs to register the points transaction
                            //Needs to register the points transaction
                            this._businessObjects.Transactions.Post(new Guid(MembershipConfigValues.BaseCommerceId), user.Id, TransactionTypes.AddPoints, _localizer["OpenWalletRewardTitle", (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue)].Value, _localizer["OpenWalletRewardDescription"].Value,
                                    0, 0, null, null, TransactionOrigins.JoinClub, true, null, "", null, DateTime.UtcNow, null, true, true, 0, 0, DealTypes.None, null, PointsEarnStatuses.DirectlyGranted, (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue), true);

                            //this._businessObjects.Transactions.Post(new Guid(MembershipConfigValues.BaseCommerceId), user.Id, TransactionTypes.AddPoints, _localizer["OpenWalletRewardTitle", (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue)].Value, _localizer["OpenWalletRewardDescription"].Value,
                            //        0, 0, 0, null, null, TransactionOrigins.JoinClub, true, null, "", null, null, DateTime.UtcNow, null, null, true, true, 0, 0, "", "", "", "", DealTypes.None, null, "", PointsEarnStatuses.DirectlyGranted, (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue), true, false);

                        }

                        //NEEDS TO CREATE THE ACCOUNT CODE, USED TO INVITE NEW USERS AND EARN POINTS
                        string accountCode = ShareAccountLogic.AssignAccountCode(this._businessObjects, userData.Id, userData.Name, userData.Email);

                        userIdentification = new UserIdentificationWithPassword
                        {
                            Id = userData.Id,
                            Email = userData.Email,
                            TempPassword = model.Password,
                            PhoneValidationRequired = true//Since it's a new user
                        };

                    }
                    else
                    {
                        userIdentification = null;
                    }
                }
                catch (Exception e)
                {
                    userIdentification = null;

                    errorMsg = "ERROR: An exception occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
            }

            return userIdentification;

        }//POST ENDS ----------------------------------------------------------------------------------------------------------//


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AppleLogin(AppleUserData model)
        {
            IActionResult result;
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (!string.IsNullOrWhiteSpace(model.IdentityToken))
                {
                    AppleUserExtractedData userModel = null;
                    UserIdentificationWithPassword userIdentification = null;
                    AppUser u = null;

                    var randomPassword = PasswordGenerator.Generate(14);

                    //Trim apple user id
                    model.AppleUserId = model.AppleUserId.Trim();

                    //If the Apple is valid

                    if (!string.IsNullOrWhiteSpace(model.Email) && model.Email.CompareTo("N/A") != 0)
                    {
                        //If it's the very 1st time user logs in

                        // Check if the user is already registered
                        u = await this._userManager.FindByNameAsync(model.Email);

                        // If not, register it
                        if (u == null)
                        {

                            userModel = new AppleUserExtractedData
                            {
                                AppleId = model.AppleUserId,
                                Email = model.Email,
                                Password = randomPassword,
                                Name = model.GivenName,
                                Gender = "-"
                            };

                            //Apple doesn't send gender
                            userModel.Gender = "-";

                            userIdentification = await CreateUserFromApple(userModel);

                            if (userIdentification != null)
                            {
                                result = Ok(userIdentification);
                            }
                            else
                            {
                                errorMsg = "Unable to create user with this apple sign-in";

                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                                //Registers the invalid call
                                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                            }

                        }
                        else
                        {//IF USER IS ALREADY REGISTERED IN THE PLATFORM

                            Initialize(Guid.Empty);

                            //Updates it's password
                            u.PasswordHash = _userManager.PasswordHasher.HashPassword(u, randomPassword);

                            //If AppleId is different, updates it
                            if (string.IsNullOrWhiteSpace(u.AppleId) || u.AppleId.CompareTo(model.AppleUserId) != 0)
                            {
                                if (!string.IsNullOrWhiteSpace(model.AppleUserId))
                                    u.AppleId = model.AppleUserId;
                            }

                            IdentityResult updateUserResult = await _userManager.UpdateAsync(u);

                            if (!updateUserResult.Succeeded)
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                            else
                            {
                                //The user password was successfully updated

                                userIdentification = new UserIdentificationWithPassword
                                {
                                    Id = u.Id,
                                    Email = u.Email,
                                    TempPassword = randomPassword,
                                    PhoneValidationRequired = !u.PhoneNumberConfirmed
                                };

                                result = Ok(userIdentification);

                                // Sign-in the user using the OWIN flow
                                var identity = new ClaimsIdentity(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);
                                identity.AddClaim(new Claim(ClaimTypes.Name, userIdentification.Email, null, "Apple"));
                                // This is very important as it will be used to populate the current user id 
                                // that is retrieved with the User.Identity.GetUserId() method inside an API Controller
                                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentification.Id, null, "LOCAL_AUTHORITY"));
                            }

                        }

                    }
                    else
                    {
                        //If it's a user that previously has logged in using Apple

                        Initialize(Guid.Empty);

                        UserData userData = this._businessObjects.Users.Get(model.AppleUserId, UserKeys.AppleUserId);

                        //If user exists but AppleId doesn't match
                        if (userData != null)
                        {

                            u = await this._userManager.FindByNameAsync(userData.Email);

                            //Updates it's password
                            u.PasswordHash = _userManager.PasswordHasher.HashPassword(u, randomPassword);
                            IdentityResult updateUserResult = await _userManager.UpdateAsync(u);

                            if (!updateUserResult.Succeeded)
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                            else
                            {
                                //The user password was successfully updated

                                userIdentification = new UserIdentificationWithPassword
                                {
                                    Id = u.Id,
                                    Email = u.Email,
                                    TempPassword = randomPassword,
                                    PhoneValidationRequired = !u.PhoneNumberConfirmed
                                };

                                result = Ok(userIdentification);

                                // Sign-in the user using the OWIN flow
                                var identity = new ClaimsIdentity(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);
                                identity.AddClaim(new Claim(ClaimTypes.Name, userIdentification.Email, null, "Facebook"));
                                // This is very important as it will be used to populate the current user id 
                                // that is retrieved with the User.Identity.GetUserId() method inside an API Controller
                                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentification.Id, null, "LOCAL_AUTHORITY"));
                            }

                        }
                        else
                        {
                            errorMsg = "Invalid Apple User Id";
                            result = new NotFoundObjectResult((
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidAppleId"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                }));

                            //Registers the invalid call
                            this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                Values.StatusCodes.NotFound, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                        }
                    }


                }
                else
                {
                    errorMsg = "No apple identity token received";
                    result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["EmptyAppleIdentityTokenIssue"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });


                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }

            }
            catch (Exception e)
            {
                errorMsg = "Error: An exception has occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;

                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }


            return result;
        }

        #endregion

        #region CONSTRUCTORS

        public AppleAccountController(UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer)
        {
            this._userManager = userManager;
            this._localizer = localizer;
        }

        #endregion
    }
}
