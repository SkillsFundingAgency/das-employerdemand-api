using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class CourseDemand : CourseDemandBase
    {
        public Guid Id { get ; set ; }
        public Location Location { get; set; }
        public Course Course { get; set; }
        
        public string StopSharingUrl { get; set; }
        
        public string StartSharingUrl { get ; set ; }
        public Guid? ExpiredCourseDemandId { get ; set ; }
        public short? EntryPoint { get ; set ; }

        public static implicit operator CourseDemand(Entities.CourseDemand source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new CourseDemand
            {
                Id = source.Id,
                OrganisationName = source.OrganisationName,
                ContactEmailAddress = source.ContactEmailAddress,
                NumberOfApprentices = source.NumberOfApprentices,
                EmailVerified = source.EmailVerified,
                Course = new Course
                {
                    Id = source.CourseId,
                    Level = source.CourseLevel,
                    Title = source.CourseTitle,
                    Route = source.CourseRoute
                },
                Location = new Location
                {
                    Name = source.LocationName,
                    Lat = source.Lat,
                    Lon = source.Long
                },
                StopSharingUrl = source.StopSharingUrl,
                Stopped = source.Stopped,
                StartSharingUrl = source.StartSharingUrl,
                ExpiredCourseDemandId = source.ExpiredCourseDemandId,
                EntryPoint = source.EntryPoint
            };
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string Route { get; set; }
    }

    public class Location
    {
        public string Name { get ; set ; }
        public double Lat { get ; set ; }
        public double Lon { get ; set ; }
    }
}