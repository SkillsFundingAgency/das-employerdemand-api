using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.EmployerDemand.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
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