using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemandByExpiredDemand
{
    public class GetCourseDemandByExpiredDemandQueryHandler : IRequestHandler<GetCourseDemandByExpiredDemandQuery, GetCourseDemandByExpiredDemandQueryResult>
    {
        private readonly ICourseDemandService _service;

        public GetCourseDemandByExpiredDemandQueryHandler (ICourseDemandService service)
        {
            _service = service;
        }
        
        public async Task<GetCourseDemandByExpiredDemandQueryResult> Handle(GetCourseDemandByExpiredDemandQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetCourseDemandByExpiredId(request.ExpiredCourseDemandId);

            return new GetCourseDemandByExpiredDemandQueryResult
            {
                CourseDemand = result
            };
        }
    }
}