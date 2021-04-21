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

    public class Location
    {
        public Location(string locationName, double lat, double lon)
        {
            Name = locationName;
            LocationPoint = new LocationPoint
            {
                GeoPoint = new List<double> {lat, lon}
            };
        }

        public string Name { get; set; }
        public LocationPoint LocationPoint { get; set; }
    }
    
    public class LocationPoint
    {
        public List<double> GeoPoint { get; set; }
    }
}