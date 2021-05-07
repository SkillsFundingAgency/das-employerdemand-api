using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;

namespace SFA.DAS.EmployerDemand.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/")]
    public class ProviderInterestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProviderInterestController> _logger;

        public ProviderInterestController(IMediator mediator, 
            ILogger<ProviderInterestController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProviderInterests(PostProviderInterestsRequest request)
        {
            try
            {
                var result = await _mediator.Send(new CreateProviderInterestsCommand
                {
                    ProviderInterests = request
                });

                if (result.IsCreated)
                {
                    return Created("",new {result.Ukprn});    
                }

                return Accepted("", new {result.Ukprn});
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationResult.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to create provider interests");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}