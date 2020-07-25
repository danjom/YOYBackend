
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeV2Controller : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            return new OkObjectResult("employees V2 controller");
        }
    }
}
