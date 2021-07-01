using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerDemandsOlderThan3Years
{
    public class GetEmployerDemandsOlderThan3YearsQueryHandler : IRequestHandler<GetEmployerDemandsOlderThan3YearsQuery, GetEmployerDemandsOlderThan3YearsResult> 
    {
        private readonly ICourseDemandService _courseDemandService;

        public GetEmployerDemandsOlderThan3YearsQueryHandler(ICourseDemandService courseDemandService)
        {
            _courseDemandService = courseDemandService;
        }

        public async Task<GetEmployerDemandsOlderThan3YearsResult> Handle(GetEmployerDemandsOlderThan3YearsQuery request, CancellationToken cancellationToken)
        {
            var oldDemands = await _courseDemandService.GetDemandsOlderThan3Years();

            return new GetEmployerDemandsOlderThan3YearsResult
            {
                EmployerDemands = oldDemands
            };
        }
    }
}

