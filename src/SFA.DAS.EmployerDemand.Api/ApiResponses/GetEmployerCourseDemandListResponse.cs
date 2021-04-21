using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetEmployerCourseDemandListResponse
    {
        public IEnumerable<GetEmployerCourseDemandResponse> EmployerCourseDemands { get; set; }
        public int TotalFiltered { get ; set ; }
        public int Total { get ; set ; }
    }
}