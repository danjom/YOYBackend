using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YOY.DAO.Entities;
using YOY.Values;
using YOY.UserAPI.Models.v1.IdentityModel;
using Microsoft.Extensions.Localization;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.UserAPI.Logic.Account;
using YOY.UserAPI.Models.v1.ThirdpartyLogin.User.POCO;
using YOY.UserAPI.Models.v1.ThirdpartyLogin.Facebook.POCO;
using YOY.DTO.Entities;
using System.Net.Http;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class FacebookAccountController : ControllerBase
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

        private readonly UserManager<AppUser> _userManager;
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

        private async Task<FacebookUserViewModel> VerifyFacebookAccessToken(string accessToken)
        {
            FacebookUserViewModel fbUser = null;

            var path = "https://graph.facebook.com/me?scope=email&&access_token=" + accessToken;
            var client = new HttpClient();
            var uri = new Uri(path);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserViewModel>(content);

                path = "https://graph.facebook.com/" + fbUser.ID + "?access_token=" + accessToken + "&fields=id,first_name,last_name,birthday,email,gender,link,picture&type=large";
                uri = new Uri(path);
                response = await client.GetAsync(uri);
                content = await response.Content.ReadAsStringAsync();
                fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserViewModel>(content);

            }

            return fbUser;
        }

        private async Task<UserIdentificationWithPassword> CreateUserFromFB(FbUserExtractedData model)
        {
            string errorMsg;
            int callId = 1;
            string parameters = model.ToString();

            UserIdentificationWithPassword userIdentification;
            if (!ModelState.IsValid)
            {
                userIdentification = null;
            }
            else
            {
                try
                {

                    DateTime? dateBirth = null;

                    if (model.DateOfBirth != null && model.DateOfBirth != DateTime.MinValue)
                    {
                        dateBirth = model.DateOfBirth.Value.ToUniversalTime();
                    }

                    AppUser user = new AppUser
                    {
                        UserName = model.Email,
                        Name = model.Name,
                        DateOfBirth = dateBirth,
                        Email = model.Email,
                        EmailConfirmed = true,
                        Gender = model.Gender,
                        FBId = model.FbId,
                        AppleId = "",
                        GoogleId = "",
                        ProfilePicUrl = model.ProfilePicUrl,
                        StateId = null,
                        AccountCode = model.Email.Length > 45 ? model.Email.Substring(0, 45) : model.Email,//Temporary
                    };

                    IdentityResult addUserResult = await this._userManager.CreateAsync(user, model.Password);

                    if (!addUserResult.Succeeded)
                    {
                        userIdentification = null;
                    }

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

                        }

                        //NEEDS TO CREATE THE ACCOUNT CODE, USED TO INVITE NEW USERS AND EARN POINTS
                        string accountCode = ShareAccountLogic.AssignAccountCode(this._businessObjects, userData.Id, userData.Name, userData.Email);

                        userIdentification = new UserIdentificationWithPassword
                        {
                            Id = userData.Id,
                            Email = userData.Email,
                            TempPassword = model.Password,
                            PhoneValidationRequired = true//because is a new user
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
        }//CREATE USER FROM FB ENDS ----------------------------------------------------------------------------------------------------------//


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post([FromBody] FbSDKData model)
        {
            IActionResult result;
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (!string.IsNullOrWhiteSpace(model.FbToken))
                {
                    //var tokenExpirationTimeSpan = TimeSpan.FromDays(14);
                    AppUser u;
                    UserIdentificationWithPassword userIdentification = null;

                    // Get the fb access token and make a graph call to the /me endpoint
                    FacebookUserViewModel fbUser = await VerifyFacebookAccessToken(model.FbToken);

                    if (fbUser == null)
                    {
                        errorMsg = "Facebook account not found";
                        result = new NotFoundObjectResult((
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["FacebookAccountNotFound"].Value,
                                    MsgTitle = "",
                                    MsgContent = ""
                                }));

                        //Registers the invalid call
                        this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                            Values.StatusCodes.NotFound, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                    }
                    else
                    {

                        // Check if the user is already registered
                        u = await this._userManager.FindByNameAsync(fbUser.Email);

                        var randomPassword = PasswordGenerator.Generate(14);

                        // If not, register it
                        if (u == null)
                        {

                            FbUserExtractedData userModel = new FbUserExtractedData
                            {
                                FbId = fbUser.ID,
                                Email = fbUser.Email,
                                Password = randomPassword,
                                Name = fbUser.FirstName,
                                DateOfBirth = fbUser.BirthDay,
                                Gender = fbUser.Gender,
                                ProfilePicUrl = fbUser.Picture
                            };

                            if (!string.IsNullOrWhiteSpace(fbUser.Gender))
                            {
                                if (fbUser.Gender.ToUpper()[0] == ProfileGenders.Male || fbUser.Gender.ToUpper()[0] == ProfileGenders.Female)
                                {
                                    userModel.Gender = fbUser.Gender.ToUpper().ToCharArray()[0] + "";
                                }
                                else
                                {
                                    userModel.Gender = ProfileGenders.NotSpecified + "";
                                }
                            }
                            else
                            {
                                userModel.Gender = ProfileGenders.NotSpecified + "";
                            }

                            userIdentification = await CreateUserFromFB(userModel);

                            if (userIdentification != null)
                            {
                                result = Ok(userIdentification);
                            }
                            else
                            {
                                errorMsg = "Unable to create user with this facebook account";
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                                //Registers the invalid call
                                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                            }

                        }
                        else
                        {//IF USER IS ALREADY REGISTERED IN THE PLATFORM

                            Initialize(Guid.Empty);

                            //Updates it's password
                            u.PasswordHash = _userManager.PasswordHasher.HashPassword(u, randomPassword);
                            IdentityResult updateUserResult = await _userManager.UpdateAsync(u);


                            //If FBId is different, updates it
                            if (string.IsNullOrWhiteSpace(u.FBId) || u.FBId.CompareTo(model.FbToken) != 0)
                            {
                                if (!string.IsNullOrWhiteSpace(model.FbToken))
                                    u.FBId = model.FbToken;
                            }

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
                            }

                        }
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
                    errorMsg = "Invalid FB token";
                    result = new BadRequestObjectResult((
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidFBToken"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                }));

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }

            }
            catch (Exception e)
            {
                errorMsg = "Error: An exception has occured, " + e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }


            return result;
        }

        #endregion

        #region CONSTRUCTORS

        public FacebookAccountController(UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer)
        {
            this._userManager = userManager;
            this._localizer = localizer;
        }

        #endregion


    }
}
