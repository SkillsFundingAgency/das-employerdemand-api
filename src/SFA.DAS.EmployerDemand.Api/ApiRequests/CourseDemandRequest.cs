using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiRequests
{
    public class CourseDemandRequest
    {
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public int NumberOfApprentices { get ; set ; }
        public Course Course { get; set; }
        public Location Location { get; set; }
        public string StopSharingUrl { get; set; }
        public string StartSharingUrl { get ; set ; }
        public Guid? ExpiredCourseDemandId { get ; set ; }
        public EntryPoint? EntryPoint { get; set; }
    }

    public class Course
    {
        public int Id { get ; set ; }
        public string Title { get ; set ; }
        public int Level { get ; set ; }
        public string Route { get; set; }
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

    public enum EntryPoint
    {
        Shortlist = 0,
        CourseDetail = 1,
        Providers = 2,
        ProviderDetail = 3
    }
}