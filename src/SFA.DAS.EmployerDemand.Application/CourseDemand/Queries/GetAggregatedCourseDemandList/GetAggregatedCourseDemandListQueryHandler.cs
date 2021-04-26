using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList
{
    public class GetAggregatedCourseDemandListQueryHandler : IRequestHandler<GetAggregatedCourseDemandListQuery, GetAggregatedCourseDemandListResult> 
    {
        private readonly ICourseDemandService _courseDemandService;

        public GetAggregatedCourseDemandListQueryHandler(ICourseDemandService courseDemandService)
        {
            _courseDemandService = courseDemandService;
        }

        public async Task<GetAggregatedCourseDemandListResult> Handle(GetAggregatedCourseDemandListQuery request, CancellationToken cancellationToken)
        {
            var total = await _courseDemandService.GetAggregatedDemandTotal(request.Ukprn);
            var result = await  _courseDemandService.GetAggregatedCourseDemandList(request.Ukprn, request.CourseId, request.Lat, request.Lon, request.Radius, new List<string>());

            return new GetAggregatedCourseDemandListResult
            {
                AggregatedCourseDemandList = result,
                Total = total
            };
        }
    }
}
