using System.Collections.Generic;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList
{
    public class GetEmployerCourseDemandListResult
    {
        public IEnumerable<EmployerCourseDemand> CourseDemands { get ; set ; }
        public int Total { get ; set ; }
    }
}