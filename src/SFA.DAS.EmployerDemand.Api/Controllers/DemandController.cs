using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.EmployerDemand.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/")]
    public class DemandController : ControllerBase
    {
        [HttpGet]
        [Route("show")]
        public IActionResult ShowDemand()
        {
            return Ok();
        }
    }
}