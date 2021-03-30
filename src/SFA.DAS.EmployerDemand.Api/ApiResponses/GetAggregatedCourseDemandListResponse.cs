using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetAggregatedCourseDemandListResponse
    {
        public IEnumerable<GetAggregatedCourseDemandSummaryResponse> AggregatedCourseDemandList { get; set; }
    }
}