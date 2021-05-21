using System;
using System.Collections.Generic;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetEmployerCourseDemandResponse
    {
        public Guid Id { get ; set ; }
        public int ApprenticesCount { get ; set ; }
        public Location Location { get ; set ; }

        public static implicit operator GetEmployerCourseDemandResponse(EmployerCourseDemand source)
        {
            return new GetEmployerCourseDemandResponse
            {
                Id = source.Id,
                ApprenticesCount = source.ApprenticesCount,
                Location = new Location(source.LocationName, source.Lat, source.Long),
            };
        }
    }
}