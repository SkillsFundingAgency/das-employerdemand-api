using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerDemandsOlderThan3Years
{
    public class GetEmployerDemandsOlderThan3YearsResult
    {
        public IEnumerable<Domain.Models.CourseDemand> EmployerDemands { get; set; }
    }
}