using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList
{
    public class GetEmployerCourseDemandListQueryHandler : IRequestHandler<GetEmployerCourseDemandListQuery, GetEmployerCourseDemandListResult>
    {
        private readonly ICourseDemandService _courseDemandService;

        public GetEmployerCourseDemandListQueryHandler (ICourseDemandService courseDemandService)
        {
            _courseDemandService = courseDemandService;
        }
        public async Task<GetEmployerCourseDemandListResult> Handle(GetEmployerCourseDemandListQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseDemandService.GetEmployerCourseDemand(request.Ukprn, request.CourseId,
                request.Lat, request.Lon, request.Radius);

            var totalResult = await _courseDemandService.GetTotalEmployerCourseDemands(request.Ukprn, request.CourseId);

            return new GetEmployerCourseDemandListResult
            {
                CourseDemands = result,
                Total = totalResult
            };
        }
    }
}