using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.StopCourseDemand
{
    public class StopCourseDemandCommandHandler : IRequestHandler<StopCourseDemandCommand, StopCourseDemandCommandResult> 
    {
        private readonly ICourseDemandService _courseDemandService;

        public StopCourseDemandCommandHandler(ICourseDemandService courseDemandService)
        {
            _courseDemandService = courseDemandService;
        }

        public async Task<StopCourseDemandCommandResult> Handle(StopCourseDemandCommand request, CancellationToken cancellationToken)
        {
            var demand = await _courseDemandService.StopCourseDemand(request.Id);

            return new StopCourseDemandCommandResult
            {
                CourseDemand = demand
            };
        }
    }
}