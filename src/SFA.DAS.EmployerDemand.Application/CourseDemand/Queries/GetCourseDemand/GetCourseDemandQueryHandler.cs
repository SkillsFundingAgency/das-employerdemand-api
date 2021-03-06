using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemand
{
    public class GetCourseDemandQueryHandler : IRequestHandler<GetCourseDemandQuery, GetCourseDemandQueryResult>
    {
        private readonly ICourseDemandService _demandService;

        public GetCourseDemandQueryHandler (ICourseDemandService demandService)
        {
            _demandService = demandService;
        }
        public async Task<GetCourseDemandQueryResult> Handle(GetCourseDemandQuery request, CancellationToken cancellationToken)
        {
            var result = await _demandService.GetCourseDemand(request.Id);

            return new GetCourseDemandQueryResult
            {
                CourseDemand = result
            };
        }
    }
}