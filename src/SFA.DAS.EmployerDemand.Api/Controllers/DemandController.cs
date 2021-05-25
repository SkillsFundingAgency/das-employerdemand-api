using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.VerifyCourseDemandEmail;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList;
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
        [Route("{id}")]
        public async Task<IActionResult> CreateDemand([FromRoute] Guid id, [FromBody] CourseDemandRequest request)
        {
            try
            {
                var result = await _mediator.Send(new CreateCourseDemandCommand
                {
                    CourseDemand = new CourseDemand
                    {
                        Id = id,
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
        public async Task<IActionResult> GetAggregatedCourseDemandList(int ukprn, [FromQuery]int? courseId, [FromQuery] double? lat, [FromQuery]double? lon, [FromQuery]int? radius, [FromQuery]IList<string> routes)
        {
            var resultFromMediator = await _mediator.Send(new GetAggregatedCourseDemandListQuery
            {
                Ukprn = ukprn,
                Lat = lat,
                Lon = lon,
                Radius = radius,
                CourseId = courseId,
                Routes = routes
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

        [HttpGet]
        [Route("providers/{ukprn}/courses/{courseId}")]
        public async Task<IActionResult> GetEmployerCourseDemandByCourse(int ukprn, int courseId, [FromQuery] double? lat, [FromQuery] double? lon, [FromQuery] int? radius)
        {
            var result = await _mediator.Send(new GetEmployerCourseDemandListQuery
            {
                Ukprn = ukprn,
                CourseId = courseId,
                Lat = lat,
                Lon = lon,
                Radius = radius
            });

            var employerDemands = result
                .CourseDemands
                .Select(demands => (GetEmployerCourseDemandResponse) demands)
                .ToList();

            var response = new GetEmployerCourseDemandListResponse
            {
                EmployerCourseDemands = employerDemands,
                Total = result.Total,
                TotalFiltered = employerDemands.Count
            };
            
            return Ok(response);
        }

        [HttpPost]
        [Route("{id}/verify-email")]
        public async Task<IActionResult> VerifyEmployerDemandEmail(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new VerifyCourseDemandEmailCommand
                {
                    Id = id
                });

                if (result.Id == null)
                {
                    return NotFound();
                }

                return Accepted("", new {result.Id});
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to verify course demand email");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployerCourseDemand(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseDemandQuery
                {
                    Id = id
                });

                if (result.CourseDemand == null)
                {
                    return NotFound();
                }

                var model = (GetCourseDemandResponse) result.CourseDemand;

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"Unable to get course demand {id}");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}
