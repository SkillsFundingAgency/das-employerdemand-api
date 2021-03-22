using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class CourseDemand
    {
        public Guid Id { get ; set ; }
        public Location Location { get; set; }
        public Course Course { get; set; }
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public int NumberOfApprentices { get ; set ; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
    }

    public class Location
    {
        public string Name { get ; set ; }
        public double Lat { get ; set ; }
        public double Lon { get ; set ; }
    }
}