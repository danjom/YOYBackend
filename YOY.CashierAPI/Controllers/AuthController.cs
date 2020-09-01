using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using YOY.CashierAPI.Config;
using YOY.CashierAPI.Handlers.Authentication;
using YOY.CashierAPI.Models.Authentication;
using YOY.CashierAPI.Models.v1.IdentityModel;
using YOY.CashierAPI.Models.v1.Miscellaneous.POCO;
using YOY.DAO.Entities;
using YOY.DTO.Entities.Misc.User;
using YOY.Values;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YOY.CashierAPI.Controllers
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
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = true,
                                    DevError = _localizer["InvalidCredentials"].Value,
                                    MsgContent = _localizer["WrongUsernameOrPassword"].Value,
                                    MsgTitle = _localizer["WrongUsernameOrPasswordTitle"].Value
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
                                Name = dataForToken.Name,
                                ProfilePicUrl = dataForToken?.ProfilePic ?? "",
                                Language = dataForToken.Language,
                                Token = token.ToString(),
                                RefreshToken = refreshToken.Value,
                                RefreshTokenExpirationUtcDate = refreshToken.ExpiresUTC
                            };
                        }
                    }
                    else
                    {
                        if (valid && !identityUser.EmailConfirmed && !identityUser.PhoneNumberConfirmed)
                        {
                            return new UnauthorizedObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.Unauthorized,
                                    CustomAction = UserappErrorCustomActions.None,
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


                if (refreshTokenGenerator.RevokeToken(model.userName, model.refreshToken))
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