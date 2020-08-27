using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOY.DAO.Entities;
using YOY.DTO.Entities.Misc.User;
using YOY.Values;
using YOY.UserAPI.Models.v1.User.POCO;
using YOY.UserAPI.Models.v1.IdentityModel;
using Microsoft.AspNetCore.Identity;
using YOY.UserAPI.Logic.Account;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.IO;
using YOY.DTO.Entities;
using Microsoft.AspNetCore.Hosting;
using YOY.ThirdpartyServices.ResponseModels.Image;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.ThirdpartyServices.Services.Image.Repo;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
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
        private ImageHandler _imgHandler;
        private UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IWebHostEnvironment _env;

        private const int controllerVersion = 1;
        private readonly string[] months;

        private string allowedImgFormats = "data:image/png;base64,";
        private string baseImgStorageUrl = "https://res.cloudinary.com/yoyimgs/image/upload/v";

        private const int minPersonalIdDaysLock = 900;
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

            if (_imgHandler == null)
                _imgHandler = new ImageHandler();
        }

        private string GetMembershipLevelName(int level)
        {
            return level switch
            {
                MembershipLevels.Bronze => _localizer["Bronze"].Value,
                MembershipLevels.Silver => _localizer["Silver"].Value,
                MembershipLevels.Gold => _localizer["Gold"].Value,
                MembershipLevels.Platinum => _localizer["Platinum"].Value,
                MembershipLevels.Diamond => _localizer["Diamond"].Value,
                _ => "--"
            };
        }

        private ProfilePicData SaveBase64Img(string base64String)
        {
            System.Drawing.Image image;
            ProfilePicData picData = null;
            int callId = 0;

            try
            {
                // Convert base 64 string to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                // Convert byte[] to Image
                using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                image = System.Drawing.Image.FromStream(ms);

                var directoryPath = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string extension = Path.GetExtension(".png");
                string path = Path.Combine(directoryPath, Guid.NewGuid().ToString() + extension);
                image.Save(path, System.Drawing.Imaging.ImageFormat.Png); // Save file into disk

                using (var streamBitmap = new MemoryStream(imageBytes))
                {
                    using (image = System.Drawing.Image.FromStream(streamBitmap))
                    {
                        image.Save(path);

                        picData = new ProfilePicData
                        {
                            Path = path,
                            Height = image.Height,
                            Width = image.Width
                        };
                    }
                }

                
            }
            catch (Exception e)
            {
                string errorMsg = "Error: An error ocurred while images was being created, " + (e.InnerException != null ? e.InnerException.Message : e.Message);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(this._businessObjects.Tenant.TenantId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, "", 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }


            return picData;

        }

        private string SaveImage(string base64String)
        {

            string imgUrl = "";

            base64String = base64String.Trim(new char[] { '\r', '\t', '\n' }).Replace("\\\\", "\\").Replace('-', '+').Replace('_', '/');


            string enviromentFolder;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                enviromentFolder = ImageFolders.DevFolder;

            }
            else
            {
                enviromentFolder = ImageFolders.ProdFolder;
            }


            ProfilePicData picData = this.SaveBase64Img(base64String);

            if(picData != null && !string.IsNullOrEmpty(picData.Path))
            {
                string extension = Path.GetExtension(".png");
                UploadResponse response = CloudinaryStorage.UploadImage(picData.Path, enviromentFolder, ImageFolders.ProfilePics, "0", picData.Width, picData.Height);
                FileInfo fileDel = new FileInfo(picData.Path);
                fileDel.Delete();


                if (response != null)
                {
                    imgUrl = baseImgStorageUrl + response.Version + "/" + response.PublicId + "." + response.Format;
                }
            }
            

            return imgUrl;
        }

        private bool ValidateInput(string input, string regex)
        {

            Regex re = new Regex(regex);
            if (re.IsMatch(input))
                return (true);
            else
                return (false);
        }
        private async Task<AppUser> ValidateUser(string email, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(email);
            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, password);
                return (result == PasswordVerificationResult.Failed ? null : identityUser);
            }

            return null;
        }

        /// <summary>
        /// Retrieve user profile information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public IActionResult Get(string userId)
        {
            int callId = 1;
            string parameters = "Account UserId: " + userId;
            string errorMsg;
            IActionResult result;
            try
            {
                Initialize(Guid.Empty);

                UserWithLocationAndMembershipData userData = this._businessObjects.Users.Get(userId, UserKeys.UserId, 0);
                if (userData != null)
                {

                    UserProfile profile = new UserProfile
                    {
                        Id = userData.Id,
                        AccountNumber = userData.AccountNumber,
                        PersonalId = userData.PersonalId,
                        PersonalIdLabel = "",
                        Name = userData.Name,
                        ValidBirthDate = userData.DateOfBirth != null && userData.DateOfBirth != DateTime.MinValue,
                        BirthDate = userData.DateOfBirth ?? DateTime.MinValue,
                        FriendlyBirthDate = "-",
                        CountryPhonePrefix = userData.CountryPhonePrefix,
                        PhoneNumber = userData.PhoneNumber,
                        PhoneNumberConfirmed = userData.PhoneNumberConfirmed,
                        EmailConfirmed = userData.EmailConfirmed,
                        Email = userData.Email,
                        Gender = userData.GenderName,
                        GenderAbbr = userData.Gender,
                        StateName = userData.StateName ?? "",
                        CountryFlag = userData.CountryFlag ?? "",
                        MembershipLevel = userData.MembershipLevel,
                        MembershipLevelName = GetMembershipLevelName(userData.MembershipLevel),
                        WalletAmount = "",
                    };

                    switch (userData.CountryCode)
                    {
                        case "CRC":
                            profile.PersonalIdLabel = "Tu cédula";
                            break;
                        case "MX":
                            profile.PersonalIdLabel = "Tu INE";
                            break;
                    }

                    switch (userData.CurrencyType)
                    {
                        case CurrencyTypes.MexicanPeso:

                            //The points are stored in dollars equivalent, needs to be converted to user's country currency

                            profile.WalletAmount = userData.CurrencySymbol + Math.Round((((decimal)userData.AvailablePoints) / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.MexicanValue, 2);

                            break;
                        case CurrencyTypes.CostaRicanColon:

                            profile.WalletAmount = userData.CurrencySymbol + Math.Round((((decimal)userData.AvailablePoints) / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.CostaRicanValue, 2);

                            break;
                        case CurrencyTypes.ColombianPeso:

                            profile.WalletAmount = userData.CurrencySymbol + Math.Round((((decimal)userData.AvailablePoints) / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.ColombianValue, 2);

                            break;
                        case CurrencyTypes.USDollar:

                            profile.WalletAmount = userData.CurrencySymbol + Math.Round((((decimal)userData.AvailablePoints) / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.USValue, 2);

                            break;
                    }

                    if (string.IsNullOrWhiteSpace(profile.Gender))
                    {
                        // "-" means unspecified gender
                        profile.Gender = _localizer["NotSpecified"].Value;
                        profile.GenderAbbr = "-";
                    }


                    if (userData.DateOfBirth != null)
                    {
                        DateTime date = (DateTime)userData.DateOfBirth;

                        int month = date.Month;

                        string day = date.Day + "";
                        if (day.Length < 2)
                        {
                            day = 0 + day;
                        }

                        profile.FriendlyBirthDate = day + "/" + months[month - 1] + "/" + date.Year;
                    }

                    result = Ok(profile);
                }
                else
                {
                    errorMsg = "Error: Invalid user";

                    result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(userData?.Id, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }



            return result;

        }//GET ENDS -------------------------------------------------------------------------------------------------------//




        /// <summary>
        /// Retrieve user profile information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("retrieveId")]
        public async Task<IActionResult> RetrieveIdAsync([FromBody] UserCredentials model)
        {
            int callId = 1;
            string parameters = "Email: " + model.Email + ", Password: "+ model.Password;
            string errorMsg;
            IActionResult result;
            try
            {
                Initialize(Guid.Empty);


                AppUser user = await ValidateUser(model.Email, model.Password);

                if(user != null)
                {
                    result = Ok(new UserId { Id = user.Id });
                }
                else
                {
                    errorMsg = "Error: Invalid user";

                    result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }



            return result;

        }//GET ENDS -------------------------------------------------------------------------------------------------------//


        /// <summary>
        /// Creates a new user account and retrieve the profile created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserModel model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, data received";
                result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = _localizer["InvalidSignupData"].Value,
                                    MsgTitle = _localizer["InvalidSignupDataTitle"].Value
                                });

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }
            else
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                string passwordRegex = "^(?=.*\\d)(?=.*\\W).{4,15}$";

                if (this.ValidateInput(model.Email, emailRegex) && this.ValidateInput(model.Password, passwordRegex))
                {

                    //1st validates if user is valid
                    AppUser existingUser = await _userManager.FindByEmailAsync(model.Email);

                    if(existingUser == null)
                    {
                        try
                        {

                            DateTime? dateBirth = null;

                            AppUser user = new AppUser
                            {
                                UserName = model.Email,
                                Name = model.Name,
                                DateOfBirth = dateBirth,
                                Email = model.Email,
                                EmailConfirmed = false,
                                PhoneNumber = "-",
                                PhoneNumberConfirmed = false,
                                Gender = !string.IsNullOrWhiteSpace(model.Gender) ? model.Gender.ToUpper().ToCharArray()[0] + "" : "-",
                                FBId = "",//From SignUp from there is no FBId
                                AppleId = "",//From SignUp from there is no AppleId
                                GoogleId = "",//From SignUp from there is no GoogleId
                                StateId = null,
                                AccountCode = model.Email.Length > 45 ? model.Email.Substring(0, 45) : model.Email,//Temporary
                                InvitorUserId = null,
                                ReferenceCode = ""
                            };

                            IdentityResult addUserResult = await this._userManager.CreateAsync(user, model.Password);

                            if (!addUserResult.Succeeded)
                            {
                                errorMsg = "";
                                foreach (IdentityError item in addUserResult.Errors)
                                {
                                    errorMsg = item.Description + "\n";
                                }

                                result = new BadRequestObjectResult(new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = errorMsg,
                                    MsgContent = _localizer["AccountCreationFailed"].Value,
                                    MsgTitle = _localizer["AccountCreationFailedTitle"].Value
                                });

                                //Registers the invalid call
                                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                            }
                            else
                            {


                                //The membership is created to link user the default commerce
                                Membership customerMembership = this._businessObjects.Memberships.Post(user.Id, UserKeys.UserId, new Guid(MembershipConfigValues.BaseCommerceId), MembershipCreationReasonTypes.OpenWallet, (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue), MembershipPointsOperationObjectiveTypes.YOYWallet);//ORIGIN IS MOBILE APP

                                if (customerMembership != null)
                                {
                                    //Needs to register the points transaction
                                    this._businessObjects.Transactions.Post(new Guid(MembershipConfigValues.BaseCommerceId), user.Id, TransactionTypes.AddPoints, _localizer["OpenWalletRewardTitle", (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue)].Value, _localizer["OpenWalletRewardDescription"].Value,
                                            0, 0, null, null, TransactionOrigins.JoinClub, true, null, "", null, DateTime.UtcNow, null, true, true,0, 0, DealTypes.None, null, PointsEarnStatuses.DirectlyGranted, (int)(MembershipConfigValues.OpenWalletUSValue * MembershipConfigValues.WalletPointsPerUSValue), true);

                                }


                                //NEEDS TO CREATE THE ACCOUNT CODE, USED TO INVITE NEW USERS AND EARN POINTS
                                string accountCode = ShareAccountLogic.AssignAccountCode(this._businessObjects, user.Id, user.Name, user.Email);


                                NewUserIdentification newUserData = new NewUserIdentification
                                {
                                    Id = user.Id,
                                    Email = user.Email
                                };

                                result = Ok(newUserData);

                            }
                        }
                        catch (Exception e)
                        {
                            errorMsg = "ERROR: An exception occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                            //Registers the invalid call
                            this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);

                        }
                    }
                    else
                    {
                        if(!existingUser.EmailConfirmed && !existingUser.PhoneNumberConfirmed)
                        {
                            UserData existingUserDate = this._businessObjects.Users.Get(existingUser.Id, UserKeys.UserId);

                            //If the account was created less than 2 days
                            if((DateTime.UtcNow - existingUserDate.CreatedDate).TotalHours < 48)
                            {

                                //Removes all password
                                IdentityResult updateUserResult = await _userManager.RemovePasswordAsync(existingUser);

                                if (updateUserResult.Succeeded)
                                {
                                    //Removed Password Success, sets the new password
                                    updateUserResult = await _userManager.AddPasswordAsync(existingUser, model.Password);

                                    if (updateUserResult.Succeeded)
                                    {
                                        //Then updates the profile
                                        existingUser.Name = model.Name;
                                        existingUser.UserName = model.Email;
                                        existingUser.Email = model.Email;
                                        existingUser.Gender = !string.IsNullOrWhiteSpace(model.Gender) ? model.Gender.ToUpper().ToCharArray()[0] + "" : "-";

                                        updateUserResult = await _userManager.UpdateAsync(existingUser);

                                        if (updateUserResult.Succeeded)
                                        {
                                            NewUserIdentification newUserData = new NewUserIdentification
                                            {
                                                Id = existingUser.Id,
                                                Email = existingUser.Email
                                            };

                                            result = Ok(newUserData);
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
                                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                                }

                            }
                            else
                            {
                                errorMsg = "Email vinculado a otra cuenta";

                                result = new ConflictObjectResult(
                                        new BasicResponse
                                        {
                                            StatusCode = Values.StatusCodes.Conflict,
                                            CustomAction = UserappErrorCustomActions.None,
                                            DisplayMsgToUser = true,
                                            DevError = _localizer["RepeatedEmail"].Value,
                                            MsgContent = _localizer["AlreadyRegisteredEmail"].Value,
                                            MsgTitle = _localizer["AlreadyRegisteredEmailTitle"].Value
                                        });

                                //Registers the invalid call
                                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                    StatusCodes.Conflict, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                            }
                        }
                        else
                        {
                            errorMsg = "Email vinculado a otra cuenta";

                            result = new ConflictObjectResult(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.Conflict,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = true,
                                        DevError = _localizer["RepeatedEmail"].Value,
                                        MsgContent = _localizer["AlreadyRegisteredEmail"].Value,
                                        MsgTitle = _localizer["AlreadyRegisteredEmailTitle"].Value
                                    });

                            //Registers the invalid call
                            this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                                StatusCodes.Conflict, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                        }

                    }

                    
                }
                else
                {
                    errorMsg = "Either password or email doesn't have required format";
                    result = new BadRequestObjectResult(new BasicResponse
                    {
                        StatusCode = Values.StatusCodes.Conflict,
                        CustomAction = UserappErrorCustomActions.None,
                        DisplayMsgToUser = true,
                        DevError = _localizer["InvalidEmailOrPassword"].Value,
                        MsgContent = _localizer["InvalidEmailOrPassword"].Value,
                        MsgTitle = _localizer["AlreadyRegisteredEmailTitle"].Value
                    });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }


            }


            return result;
        }//POST ENDS ----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Updates an user account and retrieve the profile updated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> Put([FromBody] UpdatedUserProfile model)
        {
            IActionResult result;
            int callId = 3;
            string parameters = model.ToString();
            string errorMsg;


            Initialize(Guid.Empty);

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
                                    MsgContent = _localizer["InvalidSignupData"].Value,
                                    MsgTitle = _localizer["InvalidSignupDataTitle"].Value
                                });

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.Id, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

            }
            else
            {
                try
                {
                    IdentityResult updateUserResult = null;

                    AppUser u = await _userManager.FindByIdAsync(model.Id);

                    bool personalIdUpdated = false;
                    bool errorRelatedToPersonalId = false;
                    string latestUserId = "";

                    if (u != null)
                    {
                        u.Name = model.Name;
                        u.DateOfBirth = model.BirthDate;
                        u.UserName = model.Email;
                        u.Email = model.Email;
                        u.Gender = !string.IsNullOrWhiteSpace(model.Gender) ? model.Gender.ToUpper().ToCharArray()[0] + "" : "-";

                        if(model.PersonalId != u.PersonalId)
                        {
                            //In case personal Id is different then previus
                            if (string.IsNullOrWhiteSpace(u.PersonalId) && !string.IsNullOrWhiteSpace(model.PersonalId))
                            {

                                DateTime? latestUsageDate = this._businessObjects.Users.Get(model.PersonalId, UserIdentityValueTypes.PersonalId, ref latestUserId);

                                if (latestUsageDate == null)
                                {
                                    //PersonalId haven't been used before
                                    u.PersonalId = model.PersonalId;

                                    personalIdUpdated = true;

                                }
                                else
                                {

                                    if (((DateTime.UtcNow - (DateTime)latestUsageDate).TotalDays > minPersonalIdDaysLock) || latestUserId == model.Id)
                                    {
                                        //PersonalId haven't been used for a while enough or was the same user
                                        u.PersonalId = model.PersonalId;

                                        personalIdUpdated = true;

                                    }
                                    else
                                    {
                                        errorRelatedToPersonalId = true;
                                    }
                                }
                            }
                            else
                            {
                                if (u != null && !string.IsNullOrWhiteSpace(u.PersonalId) && string.IsNullOrWhiteSpace(model.PersonalId))
                                {
                                    //PersonalId haven't been used before
                                    u.PersonalId = "";

                                    personalIdUpdated = true;
                                }
                                else
                                {
                                    if (u != null && !string.IsNullOrWhiteSpace(u.PersonalId) && !string.IsNullOrWhiteSpace(model.PersonalId) && model.PersonalId.CompareTo(u.PersonalId) != 0)
                                    {
                                        errorRelatedToPersonalId = true;
                                    }
                                }
                            }
                        }

                        updateUserResult = await _userManager.UpdateAsync(u);

                        if (personalIdUpdated && !string.IsNullOrWhiteSpace(model.PersonalId))
                        {

                            //Registers the vinculation log
                            this._businessObjects.Users.Post(model.PersonalId, UserIdentityValueTypes.PersonalId, model.Id);

                            if(!string.IsNullOrWhiteSpace(latestUserId) && latestUserId.CompareTo(model.Id) != 0)
                            {
                                //Needs to remove the personal id from the last user
                                AppUser lastOwner = await _userManager.FindByIdAsync(latestUserId);

                                if (lastOwner != null)
                                {
                                    lastOwner.PersonalId = "";
                                    IdentityResult lastOwnerUpdateResult = await _userManager.UpdateAsync(lastOwner);

                                    if (!lastOwnerUpdateResult.Succeeded)
                                    {
                                        errorMsg = "PersonalId: " + model.PersonalId + " unlink from userId " + latestUserId + " failed";

                                        //Registers the invalid call
                                        this._businessObjects.HttpcallInvokationLogs.Post(model.Id, this.GetType().Name, callId, controllerVersion,
                                                            StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                    }
                                }
                            }

                        }

                        if (updateUserResult.Succeeded)
                        {
                            UserWithLocationAndMembershipData userData = this._businessObjects.Users.Get(model.Id, true);

                            UserProfile profile = new UserProfile
                            {
                                Id = u.Id,
                                AccountNumber = userData.AccountNumber,
                                PersonalId = userData.PersonalId,
                                Name = userData.Name,
                                Email = userData.Email,
                                EmailConfirmed = userData.EmailConfirmed,
                                CountryPhonePrefix = userData.CountryPhonePrefix,
                                PhoneNumber = userData.PhoneNumber ?? "-",
                                PhoneNumberConfirmed = userData.PhoneNumberConfirmed,
                                Gender = userData.GenderName,
                                GenderAbbr = userData.Gender,
                                FriendlyBirthDate = "-",
                                BirthDate = userData.DateOfBirth ?? DateTime.MinValue,
                                ValidBirthDate = userData.DateOfBirth != null && userData.DateOfBirth != DateTime.MinValue,
                                MembershipLevel = userData.MembershipLevel,
                                MembershipLevelName = GetMembershipLevelName(userData.MembershipLevel),
                                StateName = userData.StateName,
                                CountryFlag = userData.CountryFlag,
                                WalletAmount = ""
                            };


                            switch (userData.CountryCode)
                            {
                                case "CRC":
                                    profile.PersonalIdLabel = "Tu cédula";
                                    break;
                                case "MX":
                                    profile.PersonalIdLabel = "Tu INE";
                                    break;
                            }


                            profile.FriendlyBirthDate = "";

                            if (userData.DateOfBirth != null)
                            {
                                DateTime date = (DateTime)userData.DateOfBirth;

                                string month = date.Month + "";
                                if (month.Length < 2)
                                {
                                    month = 0 + month;
                                }

                                string day = date.Day + "";
                                if (day.Length < 2)
                                {
                                    day = 0 + day;
                                }

                                profile.FriendlyBirthDate = date.Year + "/" + month + "/" + date.Day;
                            }

                            userData.AvailablePoints ??= 0;

                            switch (userData.CurrencyType)
                            {
                                case CurrencyTypes.MexicanPeso:

                                    //The points are stored in dollars equivalent, needs to be converted to user's country currency

                                    profile.WalletAmount = userData.CurrencySymbol + Math.Round(((decimal)userData.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.MexicanValue, 2);

                                    break;
                                case CurrencyTypes.CostaRicanColon:

                                    profile.WalletAmount = userData.CurrencySymbol + Math.Round(((decimal)userData.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.CostaRicanValue, 2);

                                    break;
                                case CurrencyTypes.ColombianPeso:

                                    profile.WalletAmount = userData.CurrencySymbol + Math.Round(((decimal)userData.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.ColombianValue, 2);

                                    break;
                                case CurrencyTypes.USDollar:

                                    profile.WalletAmount = userData.CurrencySymbol + Math.Round(((decimal)userData.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.USValue, 2);

                                    break;
                            }


                            SuccessfulUpdatedProfile successfulUpdatedProfile = new SuccessfulUpdatedProfile
                            {
                                UserProfile = profile,
                                DisplayMsg = false,
                                MsgTitle = "",
                                MsgContent = ""
                            };

                            if (errorRelatedToPersonalId)
                            {
                                successfulUpdatedProfile.DisplayMsg = true;
                                successfulUpdatedProfile.MsgTitle = _localizer["AlreadyRegisteredPersonalIdTitle"].Value;
                                successfulUpdatedProfile.MsgContent = _localizer["AlreadyRegisteredPersonalId"].Value;
                            }

                            result = Ok(successfulUpdatedProfile);
                        }
                        else
                        {
                            errorMsg = "";
                            foreach (IdentityError item in updateUserResult.Errors)
                            {
                                errorMsg = item.Description + "\n";
                            }

                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                            //Registers the invalid call
                            this._businessObjects.HttpcallInvokationLogs.Post(model.Id, this.GetType().Name, callId, controllerVersion,
                                                StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                        }
                    }
                    else
                    {
                        result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                    }

                }
                catch (Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.InnerException != null ? e.InnerException.Message : e.Message;
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Id, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }//PUT ENDS ----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Updates a field of the user profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("putField")]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody] UserProfileUpdatedField model)
        {
            IActionResult result;
            int callId = 4;
            string parameters = model.ToString();
            string errorMsg;
            string customErroMsg = "";

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
                    bool success = false;

                    Initialize(Guid.Empty);
                    IdentityResult updateUserResult = null;

                    AppUser u = await _userManager.FindByIdAsync(model.UserId);

                    if (u != null)
                    {
                        switch (model.FieldType)
                        {
                            case UserProfileFieldTypes.BirthDate:
                                try
                                {
                                    //Format dd/mm/yyyy
                                    string[] dateComponents = model.FieldValue.Split('/');
                                    int year = -1;
                                    int month = -1;
                                    int day = -1;

                                    if (dateComponents.Length == 3)
                                    {

                                        //DAY
                                        _ = Int32.TryParse(dateComponents[0], out day);

                                        //MONTH
                                        _ = Int32.TryParse(dateComponents[1], out month);

                                        //YEAR
                                        _ = Int32.TryParse(dateComponents[2], out year);

                                        DateTime dateOfBirth = new DateTime(year, month, day);

                                        DateTime now = DateTime.UtcNow;
                                        int age = now.Year - dateOfBirth.Year;


                                        if (age > 10 && age < 100)
                                        {
                                            u.DateOfBirth = dateOfBirth;
                                            updateUserResult = await _userManager.UpdateAsync(u);

                                            if (updateUserResult.Succeeded)
                                                success = true;
                                            else
                                            {
                                                customErroMsg = _localizer["ErrorSettingYear"].Value;
                                                success = false;
                                            }
                                        }
                                        else
                                        {
                                            customErroMsg = _localizer["OutOfBirthdateRange"].Value;
                                        }


                                    }

                                }
                                catch (Exception e)
                                {
                                    customErroMsg = _localizer["ErrorSettingYear"].Value;

                                    success = false;

                                    errorMsg = "ERROR: Data of birth update failed: " + e.InnerException != null ? e.InnerException.Message : e.Message;

                                    //Registers the invalid call
                                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                }

                                break;
                            case UserProfileFieldTypes.InvitorReferenceCode:

                                try
                                {

                                    //It's necessary to set the invitor user id
                                    UserData invitorUser = this._businessObjects.Users.Get(model.FieldValue, UserKeys.AccountCode);

                                    if(invitorUser != null)
                                    {
                                        u.ReferenceCode = model.FieldValue;
                                        u.InvitorUserId = invitorUser.Id;
                                        updateUserResult = await _userManager.UpdateAsync(u);

                                        if (updateUserResult.Succeeded)
                                            success = true;
                                        else
                                            success = false;

                                        //Now needs to create the invitation relations
                                        UserInviteRelation inviteRelation = this._businessObjects.UserInviteRelations.Post(invitorUser.Id, u.Id, null, UserInviteRelationHerarchyLevels.FirstLevelRelation, UserInviteRelationConfigValues.JoiningFirstPurchaseMoneyUSValue, UserInviteRelationConfigValues.JoiningBonusCommissionMoneyUSValue,
                                            UserInviteRelationConfigValues.JoiningBonusCommissionPercentage, UserInviteRelationConfigValues.FirstLevelAncestorFirstPurchaseMoneyUSValue, UserInviteRelationConfigValues.FirstLevelAncestorBonusCommissionPercentage, UserInviteRelationConfigValues.JoiningBonusMonthsLifeSpan,
                                            UserInviteRelationConfigValues.AncestorBonusMonthsLifeSpan);

                                        if (success && inviteRelation != null)
                                        {
                                            //Now needs to retrieve the invitor invitation relation
                                            List<UserInviteRelation> invitorRelations = this._businessObjects.UserInviteRelations.Gets(invitorUser.Id, -1);

                                            if (invitorRelations?.Count > 0)
                                            {
                                                UserInviteRelation ancestorInvitorRelation = null;
                                                bool valid = true;

                                                foreach (UserInviteRelation item in invitorRelations)
                                                {
                                                    if (item.HerarchyLevel == UserInviteRelationHerarchyLevels.FirstLevelRelation)
                                                    {
                                                        if (ancestorInvitorRelation == null)
                                                        {
                                                            ancestorInvitorRelation = item;
                                                        }
                                                        else
                                                        {
                                                            valid = false;
                                                        }
                                                    }

                                                    if (item.HerarchyLevel != UserInviteRelationHerarchyLevels.FirstLevelRelation)
                                                    {
                                                        valid = false;
                                                    }
                                                }

                                                if (valid && ancestorInvitorRelation != null)
                                                {
                                                    //Now needs to create the relation with the ancestor's invitor
                                                    this._businessObjects.UserInviteRelations.Post(ancestorInvitorRelation.AncestorUserId, u.Id, inviteRelation.Id, UserInviteRelationHerarchyLevels.SecondLevelRelation, UserInviteRelationConfigValues.JoiningFirstPurchaseMoneyUSValue, UserInviteRelationConfigValues.JoiningBonusCommissionMoneyUSValue,
                                                        UserInviteRelationConfigValues.JoiningBonusCommissionPercentage, UserInviteRelationConfigValues.SecondLevelAncestorFirstPurchaseMoneyUSValue, UserInviteRelationConfigValues.SecondLevelAncestorBonusCommissionPercentage, UserInviteRelationConfigValues.JoiningBonusMonthsLifeSpan,
                                                        UserInviteRelationConfigValues.AncestorBonusMonthsLifeSpan);
                                                }
                                            }
                                            success = true;
                                        }
                                        else
                                        {
                                            success = false;

                                            customErroMsg = _localizer["InvalidReferenceCode"].Value;

                                            errorMsg = "ERROR: Reference Code, invitator relations stablish failed";
                                            //Registers the invalid call
                                            this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                                StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                        }

                                    }
                                    else
                                    {
                                        success = false;

                                        customErroMsg = _localizer["InvalidRefCode"].Value;
                                    }
                                }
                                catch (Exception e)
                                {
                                    success = false;

                                    customErroMsg = _localizer["ErrorSettingReferenceCode"].Value;

                                    errorMsg = "ERROR: Reference code set failed " + e.InnerException != null ? e.InnerException.Message : e.Message;
                                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                                    //Registers the invalid call
                                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                }


                                break;
                            case UserProfileFieldTypes.State:

                                try
                                {
                                    u.StateId = new Guid(model.FieldValue);
                                    updateUserResult = await _userManager.UpdateAsync(u);

                                    if (updateUserResult.Succeeded)
                                        success = true;
                                    else
                                    {
                                        customErroMsg = _localizer["ErrorSettingState"].Value;
                                        success = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    success = false;

                                    customErroMsg = _localizer["ErrorSettingState"].Value;

                                    errorMsg = "ERROR: State id set failed " + e.InnerException != null ? e.InnerException.Message : e.Message;
                                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                                    //Registers the invalid call
                                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                }

                                break;
                            case UserProfileFieldTypes.ProfilePic:

                                try
                                {
                                    u.ProfilePicUrl = this.SaveImage(model.FieldValue);

                                    if (!string.IsNullOrWhiteSpace(u.ProfilePicUrl))
                                    {
                                        updateUserResult = await _userManager.UpdateAsync(u);

                                        if (!string.IsNullOrWhiteSpace(u.ProfilePicUrl))
                                        {
                                            if (updateUserResult.Succeeded)
                                                success = true;
                                            else
                                            {
                                                customErroMsg = _localizer["ErrorSettingProfilePicture"].Value;
                                                success = false;
                                            }
                                        }
                                        else
                                        {
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        success = false;
                                    }
                                    

                                }
                                catch (Exception e)
                                {
                                    success = false;

                                    customErroMsg = _localizer["ErrorSettingProfilePicture"].Value;

                                    errorMsg = "ERROR: Profile picture set failed " + e.InnerException != null ? e.InnerException.Message : e.Message;

                                    //Registers the invalid call
                                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                }


                                break;
                            case UserProfileFieldTypes.PersonalId:

                                try
                                {

                                    if(u != null && string.IsNullOrWhiteSpace(u.PersonalId) && !string.IsNullOrWhiteSpace(model.FieldValue))
                                    {
                                        //Needs to link the personalId to user
                                        UserWithLocationAndMembershipData userWithLocationAndMembershipData = this._businessObjects.Users.Get(model.UserId, false);

                                        if(userWithLocationAndMembershipData != null)
                                        {
                                            string latestUserId = "";

                                            DateTime? latestUsageDate = this._businessObjects.Users.Get(model.FieldValue, UserIdentityValueTypes.PersonalId, ref latestUserId);

                                            if(latestUsageDate == null)
                                            {
                                                //PersonalId haven't been used before
                                                u.PersonalId = model.FieldValue;
                                                updateUserResult = await _userManager.UpdateAsync(u);

                                                if (updateUserResult.Succeeded)
                                                {
                                                    success = true;
                                                }
                                                else
                                                {
                                                    customErroMsg = _localizer["ErrorSettingPersonalId"].Value;
                                                    success = false;
                                                }

                                            }
                                            else
                                            {

                                                if (latestUserId != null && (((DateTime.UtcNow - (DateTime)latestUsageDate).TotalDays > minPersonalIdDaysLock) || latestUserId == model.UserId))
                                                {
                                                    //PersonalId haven't been used for a while enough or was the same user
                                                    u.PersonalId = model.FieldValue;
                                                    updateUserResult = await _userManager.UpdateAsync(u);

                                                    if (updateUserResult.Succeeded)
                                                    {
                                                        success = true;

                                                        //Registers the vinculation log
                                                        this._businessObjects.Users.Post(model.FieldValue, UserIdentityValueTypes.PersonalId, model.UserId);

                                                        //Needs to remove the personal id from the last user
                                                        AppUser lastOwner = await _userManager.FindByIdAsync(latestUserId);
                                                        
                                                        if(lastOwner != null)
                                                        {
                                                            lastOwner.PersonalId = "";
                                                            updateUserResult = await _userManager.UpdateAsync(lastOwner);

                                                            if (!updateUserResult.Succeeded)
                                                            {
                                                                errorMsg = "PersonalId: " + model.FieldValue + " unlink from userId " + latestUserId + " failed";

                                                                //Registers the invalid call
                                                                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        customErroMsg = _localizer["ErrorSettingPersonalId"].Value;
                                                        success = false;
                                                    }
                                                }
                                                else
                                                {
                                                    customErroMsg = _localizer["PersonalIdRecentlyUsedInOtherAccount"].Value;
                                                    success = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        if(u != null && !string.IsNullOrWhiteSpace(u.PersonalId) && string.IsNullOrWhiteSpace(model.FieldValue))
                                        {
                                            //PersonalId haven't been used before
                                            u.PersonalId = "";
                                            updateUserResult = await _userManager.UpdateAsync(u);

                                            if (updateUserResult.Succeeded)
                                                success = true;
                                            else
                                            {
                                                customErroMsg = _localizer["ErrorRemovingPersonalId"].Value;
                                                success = false;
                                            }
                                        }
                                    }
                                }
                                catch(Exception e)
                                {
                                    success = false;

                                    customErroMsg = _localizer["ErrorUpdatingProfilePicture"].Value;

                                    errorMsg = "ERROR: Personal Id set failed " + e.InnerException != null ? e.InnerException.Message : e.Message;

                                    //Registers the invalid call
                                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                                        StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                                }

                                break;
                            default:
                                success = false;

                                break;

                        }



                        if (success)
                            result = Ok(new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.Ok,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = false,
                                DevError = "",
                                MsgContent = "",
                                MsgTitle = ""
                            });
                        else
                        {
                            result = new BadRequestObjectResult(new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.BadRequest,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = true,
                                DevError = customErroMsg,
                                MsgContent = customErroMsg,
                                MsgTitle = _localizer["FailedOperation"].Value
                            });
                        }
                    }
                    else
                    {
                        result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                    }

                }
                catch (Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.InnerException != null ? e.InnerException.Message : e.Message;
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }//PUT ENDS ----------------------------------------------------------------------------------------------------------//

        
        /// <summary>
        /// Updates an user account and retrieve the profile updated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("putPassword")]
        public async Task<IActionResult> Put([FromBody] PasswordUpdate model)
        {
            IActionResult result = null;
            int callId = 5;
            string parameters = model.ToString();
            string errorMsg;


            Initialize(Guid.Empty);

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
                    IdentityResult updateUserResult = null;

                    AppUser u = await _userManager.FindByIdAsync(model.UserId);

                    //string passHash = UserManager.PasswordHasher.HashPassword(u, model.CurrentPassword);

                    //If current password matches
                    if (await _userManager.CheckPasswordAsync(u, model.CurrentPassword))
                    {

                        //Removes all password
                        updateUserResult = await _userManager.RemovePasswordAsync(u);

                        if (updateUserResult.Succeeded)
                        {
                            //Removed Password Success, sets the new password
                            updateUserResult = await _userManager.AddPasswordAsync(u, model.NewPassword);

                            if (updateUserResult.Succeeded)
                            {
                                result = Ok(new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.Ok,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = "",
                                    MsgTitle = "",
                                    MsgContent = ""
                                });
                            }
                            else
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                        }


                    }
                    else
                    {
                        errorMsg = "ERROR: Password mismatch ";
                        result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["IncorrectCurrentPassword"].Value,
                                    MsgContent = _localizer["IncorrectCurrentPassword"].Value,
                                    MsgTitle = _localizer["IncorrectCurrentPasswordTitle"].Value,
                                });
                    }

                }
                catch (Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.Message;
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }//PUT ENDS ----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Updates an user account and retrieve the profile updated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            IActionResult result = null;
            int callId = 6;
            string parameters = model.ToString();
            string errorMsg;


            Initialize(Guid.Empty);

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
                this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

            }
            else
            {
                try
                {
                    //Logic to create password recover email

                    result = Ok(new BasicResponse
                    {
                        StatusCode = Values.StatusCodes.Ok,
                        CustomAction = UserappErrorCustomActions.None,
                        DisplayMsgToUser = false,
                        DevError = "",
                        MsgTitle = "",
                        MsgContent = ""
                    });

                }
                catch (Exception e)
                {
                    errorMsg = "ERROR: An exception occured " + e.Message;
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.Email, this.GetType().Name, callId, controllerVersion,
                                        StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }//PUT ENDS ----------------------------------------------------------------------------------------------------------//



        #endregion

        #region CONSTRUCTORS
        public AccountController(UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer, IWebHostEnvironment env)
        {
            this._userManager = userManager;
            this._localizer = localizer;
            this._env = env;

            this.months = new string[] { _localizer["January"].Value, _localizer["February"].Value,
            _localizer["March"].Value, _localizer["April"].Value, _localizer["May"].Value, _localizer["June"].Value,
            _localizer["July"].Value, _localizer["August"].Value, _localizer["September"].Value, _localizer["October"].Value,
            _localizer["November"].Value, _localizer["December"].Value };
        }
        #endregion
    }
}
