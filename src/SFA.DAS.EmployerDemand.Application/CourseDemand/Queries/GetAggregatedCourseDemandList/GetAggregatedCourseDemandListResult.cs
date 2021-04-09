using System.Collections.Generic;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList
{
    public class GetAggregatedCourseDemandListResult
    {
        public IEnumerable<AggregatedCourseDemandSummary> AggregatedCourseDemandList { get; set; }
        public int Total { get ; set ; }
    }
}