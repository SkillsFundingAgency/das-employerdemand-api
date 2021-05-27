using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands
{
    public class GetUnmetEmployerDemandsQueryHandler : IRequestHandler<GetUnmetEmployerDemandsQuery, GetUnmetEmployerDemandsQueryResult>
    {
        private readonly ICourseDemandService _service;

        public GetUnmetEmployerDemandsQueryHandler (ICourseDemandService service)
        {
            _service = service;
        }
        public async Task<GetUnmetEmployerDemandsQueryResult> Handle(GetUnmetEmployerDemandsQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetUnmetEmployerDemands(request.AgeOfDemand);

            return new GetUnmetEmployerDemandsQueryResult
            {
                EmployerDemandIds = result.ToList()
            };
        }
    }
}