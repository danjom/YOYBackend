using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YOY.DAO.Entities;
using YOY.DTO.Entities.Misc.User;
using YOY.Values;
using YOY.Values.Strings;
using YOY.UserAPI.Config;
using YOY.UserAPI.Handlers.Authentication;
using YOY.UserAPI.Models.Authentication;
using YOY.UserAPI.Models.v1.IdentityModel;
using Microsoft.Extensions.Localization;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using Microsoft.Extensions.Configuration;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        private static Tenant _tenant;
        private BusinessObjects _businessObjects;
        private readonly int controllerVersion = 1;

        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IConfiguration _configuration;

        #endregion

        #region METHODS

        private void Initialize(Guid commerceId)
        {
            _tenant = Tenant.GetInstance(commerceId);
            _businessObjects = BusinessObjects.GetInstance(_tenant);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody] TokenCredentials credentials)
        {

            UserAuthDetails authDetails = null;
            string parameters = credentials.ToString();
            int callId = 1;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid)
                {

                    bool valid = false;
                    JWTTokenHandler tokenGenerator = null;
                    OwnRefreshTokenHandler refreshTokenGenerator = null;
                    AppUser identityUser;

                    if (string.IsNullOrWhiteSpace(credentials.RefreshToken) && !string.IsNullOrWhiteSpace(credentials.Password))
                    {

                        if (!ModelState.IsValid
                            || credentials == null
                            || (identityUser = await ValidateUser(credentials)) == null)
                        {
                            return new BadRequestObjectResult(
                                new BasicResponse { 
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidCredentials"].Value, 
                                    MsgContent = _localizer["WrongUsernameOrPassword"].Value,
                                    MsgTitle = _localizer["WrongUsernameOrPassword"].Value
                                });
                        }

                        valid = true;

                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(credentials.RefreshToken) && string.IsNullOrWhiteSpace(credentials.Password))
                        {
                            refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["yoyapiuser-refreshtokenSalt"], _configuration["DB-Conn"]);

                            RefreshToken refreshToken = refreshTokenGenerator.RetrieveRefreshToken(credentials.Username, credentials.ClientId, credentials.RefreshToken, DateTime.UtcNow);

                            if (refreshToken != null)
                            {
                                identityUser = await _userManager.FindByNameAsync(credentials.Username);

                                valid = true;

                            }
                            else
                            {
                                return new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidToken"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                            }
                        }
                        else
                        {
                            return new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["EmptyTokenIssue"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                        }
                    }

                    if (valid && (identityUser.PhoneNumberConfirmed || identityUser.EmailConfirmed))
                    {
                        if (tokenGenerator == null)
                            tokenGenerator = new JWTTokenHandler(jwtBearerTokenSettings, _configuration["yoyuserapi-secret"]);

                        if (refreshTokenGenerator == null)
                            refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["yoyapiuser-refreshtokenSalt"], _configuration["DB-Conn"]);


                        var token = tokenGenerator.GenerateToken(CreateClaims(identityUser.Id));


                        RefreshToken refreshToken = refreshTokenGenerator.GenerateRefreshToken(identityUser.UserName, credentials.ClientId, DateTime.UtcNow.AddHours(jwtBearerTokenSettings.RefreshTokenExpiryTimeInHours));

                        //Retrieves User Required Data
                        UserDataForToken dataForToken = this._businessObjects.Users.Get(identityUser.UserName);

                        if(dataForToken != null)
                        {
                            authDetails = new UserAuthDetails
                            {
                                UserId = dataForToken.UserId,
                                Username = dataForToken.UserName,
                                AccountNumber = dataForToken.AccountNumber,
                                AccountCode = dataForToken.AccountCode,
                                ProfilePicUrl = dataForToken.ProfilePic ?? "",
                                Name = dataForToken.Name.Split(' ')[0],
                                CountryId = dataForToken.CountryId ?? Guid.Empty,
                                Language = string.IsNullOrWhiteSpace(dataForToken.Language) ? credentials.Language : dataForToken.Language,
                                StateId = dataForToken.StateId ?? Guid.Empty,
                                StateName = dataForToken.StateName ?? "",
                                CountryFlag = dataForToken.CountryFlag ?? "",
                                //CountryCode = dataForToken.country
                                CurrencySymbol = dataForToken.CurrencySymbol ?? "",
                                CurrencyType = dataForToken.CurrencyType ?? 0,
                                AvailablePoints = dataForToken.AvailablePoints,
                                UtcTimeZone = dataForToken.StateUtcTimeZone ?? 0,
                                MembershipLevel = dataForToken.MembershipLevel,
                                ShowLocationChooser = dataForToken.StateId == null,
                                AskBirthdate = dataForToken.DateOfBirth == null,
                                AndroidVersion = dataForToken.LastestAndroidVersion,
                                iOSVersion = dataForToken.LastestiOSVersion,
                                Token = token.ToString(),
                                RefreshToken = refreshToken.Value,
                                RefreshTokenExpirationUtcDate = refreshToken.ExpiresUTC,
                                IntroVideoLink = "",
                            };

                            if (string.IsNullOrWhiteSpace(dataForToken.Language))
                            {
                                //In case language hasn't been set
                                this._businessObjects.Users.Put(dataForToken.AccountNumber, UserProfileFieldTypes.Language, credentials.Language);
                            }

                            switch (dataForToken.CurrencyType)
                            {
                                case CurrencyTypes.MexicanPeso:

                                    //The points are stored in dollars equivalent, needs to be converted to user's country currency

                                    authDetails.WalletAmount = dataForToken.CurrencySymbol + Math.Round((dataForToken.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.MexicanValue, 2);

                                    break;
                                case CurrencyTypes.CostaRicanColon:

                                    authDetails.WalletAmount = dataForToken.CurrencySymbol + Math.Round((dataForToken.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.CostaRicanValue, 2);

                                    break;
                                case CurrencyTypes.ColombianPeso:

                                    authDetails.WalletAmount = dataForToken.CurrencySymbol + Math.Round((dataForToken.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.ColombianValue, 2);

                                    break;
                                case CurrencyTypes.USDollar:

                                    authDetails.WalletAmount = dataForToken.CurrencySymbol + Math.Round((dataForToken.AvailablePoints / MembershipConfigValues.WalletPointsPerUSValue) * MoneyConversions.USValue, 2);

                                    break;
                            }
                        }

                        //Preferences values
                        int interestsCount = this._businessObjects.UserInterests.Gets(dataForToken.AccountNumber, ActiveStates.Active);

                        authDetails.ShowPrefrencesChooser = !(interestsCount > 0);

                        if(!authDetails.ShowLocationChooser && !authDetails.ShowPrefrencesChooser)
                        {
                            authDetails.AskBirthdate = false;
                        }
                    }
                    else
                    {
                        if(valid && !identityUser.EmailConfirmed && !identityUser.PhoneNumberConfirmed)
                        {
                            return new UnauthorizedObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.Unauthorized,
                                    CustomAction = UserappErrorCustomActions.PhoneValidationRequired,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["NotValidatedAccount"].Value,
                                    MsgContent = _localizer["AccountValidationNeeded"].Value,
                                    MsgTitle = _localizer["AccountValidationNeededTitle"].Value,
                                });
                        }
                    }
                }
                else
                {
                    return new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                }
            }
            catch (Exception e)
            {
                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(credentials.Username, this.GetType().Name, callId, controllerVersion,
                                    StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, e.InnerException != null ? e.InnerException.Message : e.Message);

                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

            }

            return Ok(authDetails);
        }

        [HttpPost]
        [Route("revoke-token")]
        public IActionResult RevokeToken([FromBody] TokenRevokeRequest model)
        {

            if (ModelState.IsValid)
            {
                OwnRefreshTokenHandler refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["yoyapiuser-refreshtokenSalt"], _configuration["DB-Conn"]);

               
                if(refreshTokenGenerator.RevokeToken(model.userName, model.refreshToken))
                {

                    return Ok(
                        new BasicResponse 
                        { 
                            StatusCode = Values.StatusCodes.Ok,
                            CustomAction = UserappErrorCustomActions.None,
                            DisplayMsgToUser = false,
                            DevError = "",
                            MsgContent = "",
                            MsgTitle = ""
                        });
                }
                else
                {
                    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
            }
            
        }

        private async Task<AppUser> ValidateUser(TokenCredentials credentials)
        {
            var identityUser = await _userManager.FindByNameAsync(credentials.Username);
            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return (result == PasswordVerificationResult.Failed ? null : identityUser);
            }

            return null;
        }

        private List<Claim> CreateClaims(string id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, id),
                new Claim(ClaimTypes.Role, "user")
        };

            return claims;
        }

        #endregion

        #region CONSTRUCTORS
        
        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this._userManager = userManager;
            this._localizer = localizer;
            this._configuration = configuration;
        }

        #endregion

    }
}