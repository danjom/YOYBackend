
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            return new OkObjectResult("employees V1 controller");
        }
    }
}
