﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemandNotificationAudit;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemandByExpiredDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerDemandsOlderThan3Years;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands;
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
        
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> CreateDemand([FromRoute] Guid id, [FromBody] CourseDemandRequest request)
        {
            try
            {
                short? entryPoint = null;
                
                if (request.EntryPoint.HasValue && Enum.IsDefined(typeof(EntryPoint), request.EntryPoint.Value))
                {
                    entryPoint = (short) request.EntryPoint.Value;
                }
                
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
                        },
                        StopSharingUrl = request.StopSharingUrl,
                        StartSharingUrl = request.StartSharingUrl,
                        ExpiredCourseDemandId = request.ExpiredCourseDemandId,
                        EntryPoint = entryPoint
                    }
                });
                
                if (!result.IsCreated && !result.Id.HasValue)
                {
                    return Conflict();
                }
                
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

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchDemand([FromRoute] Guid id, [FromBody] JsonPatchDocument<PatchCourseDemand> request)
        {
            try
            {
                var result = await _mediator.Send(new PatchCourseDemandCommand
                {
                    Id = id,
                    Patch = request
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
                _logger.LogError(e,"Unable to update course demand");
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmployerCourseDemandByExpiredId([FromQuery]Guid expiredCourseDemandId)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseDemandByExpiredDemandQuery()
                {
                    ExpiredCourseDemandId = expiredCourseDemandId
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
                _logger.LogError(e,$"Unable to get course demand by expired id {expiredCourseDemandId}");
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

        [HttpGet]
        [Route("unmet")]
        public async Task<IActionResult> GetUnmetCourseDemands([FromQuery]uint ageOfDemandInDays)
        {
            try
            {
                var result = await _mediator.Send(new GetUnmetEmployerDemandsQuery
                {
                    AgeOfDemandInDays = ageOfDemandInDays
                });

                var model = new GetUnmetCourseDemandResponse
                {
                    UnmetCourseDemands = result.EmployerDemands.Select(c=>(GetUnmetCourseDemand)c).ToList()
                };
                
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error getting unmet employer demands");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("older-than-3-years")]
        public async Task<IActionResult> GetDemandsOlderThan3Years()
        {
            try
            {
                var result = await _mediator.Send(new GetEmployerDemandsOlderThan3YearsQuery());

                var model = new GetDemandsOlderThan3YearsResponse
                {
                    EmployerDemandIds = result.EmployerDemands.Select(demand => demand.Id)
                };
                
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error getting employer demands older than 3 years");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("{courseDemandId}/notification-audit/{id}")]
        public async Task<IActionResult> CreateDemandNotificationAudit( Guid id, Guid courseDemandId,
            NotificationType notificationType)
        {
            try
            {
                await _mediator.Send(new CreateCourseDemandNotificationAuditCommand
                {
                    CourseDemandNotificationAudit = new CourseDemandNotificationAudit
                    {
                        Id = id,
                        CourseDemandId = courseDemandId,
                        NotificationType = notificationType
                    }
                });

                return Created("", new { id });
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error getting unmet employer demands");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}
