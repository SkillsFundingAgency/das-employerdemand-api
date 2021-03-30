using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.EmployerDemand.Api.ApiModels
{
    public class GetAggregatedCourseDemandListForProviderRequest
    {
        [FromRoute]
        public int Ukprn { get; set; }
    }
}