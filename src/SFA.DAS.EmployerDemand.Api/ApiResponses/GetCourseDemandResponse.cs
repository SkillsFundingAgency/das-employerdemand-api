using System;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetCourseDemandResponse
    {
        public Guid Id { get ; set ; }
        public Location Location { get; set; }
        public Course Course { get; set; }
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public int NumberOfApprentices { get ; set ; }
        public bool EmailVerified { get ; set ; }
        public string StopSharingUrl { get; set; }
        public bool Stopped { get; set; }
        public string StartSharingUrl { get ; set ; }
        public Guid? ExpiredCourseDemandId { get ; set ; }

        public static implicit operator GetCourseDemandResponse(Domain.Models.CourseDemand source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new GetCourseDemandResponse
            {
                Id = source.Id,
                ContactEmailAddress = source.ContactEmailAddress,
                OrganisationName = source.OrganisationName,
                NumberOfApprentices = source.NumberOfApprentices,
                EmailVerified = source.EmailVerified,
                Location = source.Location,
                Course = source.Course,
                StopSharingUrl = source.StopSharingUrl,
                Stopped = source.Stopped,
                StartSharingUrl = source.StartSharingUrl,
                ExpiredCourseDemandId = source.ExpiredCourseDemandId
            };
        }
    }
}