using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YOY.ValidationAPI.APIKeyAuth.Handlers.Authentication;
using YOY.ValidationAPI.APIKeyAuth.Models.Authentication;
using YOY.ValidationAPI.APIKeyAuth.Models.v1.ApiKey.POCO;
using YOY.Values.Strings;

namespace YOY.ValidationAPI.APIKeyAuth.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiKeyController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        [AllowAnonymous]
        [HttpPost]
        [Route("post")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public ActionResult<ApiKey> Post([FromBody] NewApiKeyData model)
        {
            ActionResult result;

            if (ModelState.IsValid)
            {
                ApiKeyHandler.SetParams(_configuration["valid8api-apiKeySalt"], _configuration["DB-Conn"]);
                ApiKey apiKey = ApiKeyHandler.GenerateApiKey(model.ClientId, model.Discriminator, model.TenantId, model.RequesterReferenceId, model.RequesterReferenceType, model.ExpirationDays);

                result =  Ok(apiKey);
            }
            else
            {
                result = BadRequest(new { message = Resources.InvalidPayload });
            }

            return result;
        }

        public ApiKeyController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
    }
}
