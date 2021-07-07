using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class CourseDemandBase
    {
        public string OrganisationName { get ; set ; }
        public string ContactEmailAddress { get ; set ; }
        public int NumberOfApprentices { get ; set ; }
        public bool EmailVerified { get ; set ; }
        public bool Stopped { get; set; }
    }
}