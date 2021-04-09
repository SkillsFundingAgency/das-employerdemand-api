using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList;
using SFA.DAS.EmployerDemand.Domain.Models;
using Course = SFA.DAS.EmployerDemand.Domain.Models.Course;
using Location = SFA.DAS.EmployerDemand.Domain.Models.Location;

namespace SFA.DAS.EmployerDemand.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/")]
    public class DemandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DemandController> _logger;

        public DemandController (IMediator mediator, ILogger<DemandController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("show")]
        public IActionResult ShowDemand()
        {
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDemand(CourseDemandRequest request)
        {
            try
            {
                var result = await _mediator.Send(new CreateCourseDemandCommand
                {
                    CourseDemand = new CourseDemand
                    {
                        Id = request.Id,
                        OrganisationName = request.OrganisationName,
                        ContactEmailAddress = request.ContactEmailAddress,
                        NumberOfApprentices = request.NumberOfApprentices,
                        Course = new Course
                        {
                            Id = request.Course.Id,
                            Title = request.Course.Title,
                            Level = request.Course.Level,
                            Route = request.Course.Route
                        },
                        Location = new Location
                        {
                            Name = request.Location.Name,
                            Lat = request.Location.LocationPoint.GeoPoint.First(),
                            Lon = request.Location.LocationPoint.GeoPoint.Last()
                        }
                    }
                });
                if (result.IsCreated)
                {
                    return Created("",new {result.Id});    
                }

                return Accepted("", new {result.Id});
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationResult.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to create course demand");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("aggregated/providers/{ukprn}")]
        public async Task<IActionResult> GetAggregatedCourseDemandList(int ukprn, [FromQuery]int? courseId, [FromQuery] double? lat, [FromQuery]double? lon, [FromQuery]int? radius)
        {
            var resultFromMediator = await _mediator.Send(new GetAggregatedCourseDemandListQuery
            {
                Ukprn = ukprn,
                Lat = lat,
                Lon = lon,
                Radius = radius,
                CourseId = courseId
            });

            var getAggregatedCourseDemandSummaryResponses = resultFromMediator.AggregatedCourseDemandList.Select(summary =>
                (GetAggregatedCourseDemandSummaryResponse) summary).ToList();
            var response = new GetAggregatedCourseDemandListResponse
            {
                AggregatedCourseDemandList = getAggregatedCourseDemandSummaryResponses,
                TotalFiltered = getAggregatedCourseDemandSummaryResponses.Count,
                Total = resultFromMediator.Total
            };

            return Ok(response);
        }
    }
}
