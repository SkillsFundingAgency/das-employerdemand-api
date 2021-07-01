using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class CourseDemand
    {
        public Guid Id { get; set; }
        public string ContactEmailAddress { get; set; }
        public string OrganisationName { get; set; }
        public int NumberOfApprentices { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int CourseLevel { get; set; }
        public string CourseRoute { get; set; }
        public string LocationName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public DateTime DateCreated { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime? DateEmailVerified { get ; set ; }
        public string StopSharingUrl { get; set; }
        public bool Stopped { get; set; }
        public DateTime? DateStopped { get ; set ; }

        public virtual ICollection<ProviderInterest> ProviderInterests { get ; set ; }
        public virtual ICollection<CourseDemandNotificationAudit> CourseDemandNotificationAudits { get ; set ; }
        public string StartSharingUrl { get ; set ; }
        public Guid? ExpiredCourseDemandId { get ; set ; }
        public virtual CourseDemand ExpiredCourseDemand { get ; set ; }
        public short? EntryPoint { get ; set ; }


        public static implicit operator CourseDemand(Models.CourseDemand source)
        {
            return new CourseDemand
            {
                Id = source.Id,
                ContactEmailAddress = source.ContactEmailAddress,
                OrganisationName = source.OrganisationName,
                NumberOfApprentices = source.NumberOfApprentices,
                EmailVerified = source.EmailVerified,
                Lat = source.Location.Lat,
                Long = source.Location.Lon,
                LocationName = source.Location.Name,
                CourseId = source.Course.Id,
                CourseTitle = source.Course.Title,
                CourseLevel = source.Course.Level,
                CourseRoute = source.Course.Route,
                StopSharingUrl = source.StopSharingUrl,
                StartSharingUrl = source.StartSharingUrl,
                ExpiredCourseDemandId = source.ExpiredCourseDemandId,
                Stopped = source.Stopped,
                EntryPoint = source.EntryPoint
            };
        }
    }
}