using System;

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
        public string LocationName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public DateTime DateCreated { get; set; }
        public bool EmailVerified { get; set; }
    }
}