using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiModels
{
    public class GetAggregatedCourseDemandListResponse
    {
        public IEnumerable<GetAggregatedCourseDemandSummaryResponse> AggregatedCourseDemandList { get; set; }
    }
}