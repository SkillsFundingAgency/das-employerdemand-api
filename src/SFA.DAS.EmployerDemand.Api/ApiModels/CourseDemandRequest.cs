using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiModels
{
    public class CourseDemandRequest
    {
        public Guid Id { get ; set ; }
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public int NumberOfApprentices { get ; set ; }
        public Course Course { get; set; }
        public Location Location { get; set; }
    }

    public class Course
    {
        public int Id { get ; set ; }
        public string Title { get ; set ; }
        public int Level { get ; set ; }
    }

    public class Location
    {
        public string Name { get ; set ; }
        public LocationPoint LocationPoint { get ; set ; }
    }

    public class LocationPoint
    {
        public List<double> GeoPoint { get; set; }
    }
}