using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using YOY.BusinessAPI.Config;
using YOY.BusinessAPI.Handlers.Authentication;
using YOY.BusinessAPI.Models.Authentication;
using YOY.BusinessAPI.Models.v1.IdentityModel;
using YOY.Values.Strings;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration _configuration;

        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
            this._configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody] TokenCredentials credentials)
        {

            UserAuthDetails authDetails = null;
            try
            {
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
                            return new BadRequestObjectResult(new { Message = Resources.InvalidCredentials });
                        }

                        valid = true;

                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(credentials.RefreshToken) && string.IsNullOrWhiteSpace(credentials.Password))
                        {
                            refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["bizapi-refreshtokenSalt"], _configuration["DB-Conn"]);

                            RefreshToken refreshToken = refreshTokenGenerator.RetrieveRefreshToken(credentials.Username, credentials.ClientId, credentials.RefreshToken, DateTime.UtcNow);

                            if(refreshToken != null)
                            {
                                identityUser = await userManager.FindByNameAsync(credentials.Username);

                                valid = true;

                            }
                            else
                            {
                                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }
                        }
                        else
                        {
                            return new BadRequestObjectResult(new { Message = Resources.InvalidCredentials });
                        }
                    }

                    if (valid && (identityUser.PhoneNumberConfirmed || identityUser.EmailConfirmed))
                    {
                        if(tokenGenerator == null)
                            tokenGenerator = new JWTTokenHandler(jwtBearerTokenSettings, _configuration["bizapi-secret"]);

                        if (refreshTokenGenerator == null)
                            refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["bizapi-refreshtokenSalt"], _configuration["DB-Conn"]);


                        var token = tokenGenerator.GenerateToken(CreateClaims(identityUser.Id));

                        
                        RefreshToken refreshToken = refreshTokenGenerator.GenerateRefreshToken(identityUser.UserName, credentials.ClientId, DateTime.UtcNow.AddHours(jwtBearerTokenSettings.RefreshTokenExpiryTimeInHours));

                        authDetails = new UserAuthDetails
                        {
                            UserId = identityUser.Id,
                            Username = identityUser.UserName,
                            Name = identityUser.Name,
                            Token = token.ToString(),
                            RefreshToken = refreshToken.Value,
                            RefreshTokenExpirationUtcDate = refreshToken.ExpiresUTC
                        };
                    }
                }
                else
                {
                    return new BadRequestObjectResult(new { Message = Resources.InvalidPayload });
                }
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(new { Message = Resources.UnexpectedError + ": " + e.InnerException });
            }

            return Ok(authDetails);
        }

        [HttpPost]
        [Route("revoke-token")]
        public IActionResult RevokeToken([FromBody] TokenRevokeRequest model)
        {

            if (ModelState.IsValid)
            {
                OwnRefreshTokenHandler refreshTokenGenerator = new OwnRefreshTokenHandler(_configuration["bizapi-refreshtokenSalt"], _configuration["DB-Conn"]);


                if (refreshTokenGenerator.RevokeToken(model.userName, model.refreshToken))
                {

                    return Ok(new { Message = Resources.LoggedOut });
                }
                else
                {
                    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return new BadRequestObjectResult(new { Message = Resources.InvalidPayload });
            }

        }

        private async Task<AppUser> ValidateUser(TokenCredentials credentials)
        {
            var identityUser = await userManager.FindByNameAsync(credentials.Username);
            if (identityUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return (result == PasswordVerificationResult.Failed ? null : identityUser);
            }

            return null;
        }

        private List<Claim> CreateClaims(string id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, id)
            };

            return claims;
        }



    }
}