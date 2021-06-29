using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand
{
    public class PatchCourseDemandCommandHandler : IRequestHandler<PatchCourseDemandCommand, PatchCourseDemandCommandResponse>
    {
        private readonly ICourseDemandService _service;

        public PatchCourseDemandCommandHandler (ICourseDemandService service)
        {
            _service = service;
        }
        public async Task<PatchCourseDemandCommandResponse> Handle(PatchCourseDemandCommand request, CancellationToken cancellationToken)
        {
            var demand = await _service.GetCourseDemand(request.Id);

            if (demand == null)
            {
                return new PatchCourseDemandCommandResponse
                {
                    Id = null
                };
            }
            
            demand.OrganisationName = request.OrganisationName;
            demand.ContactEmailAddress = request.ContactEmailAddress;
            demand.Stopped = request.Stopped;
            
            var result = await _service.UpdateCourseDemand(demand);

            return new PatchCourseDemandCommandResponse
            {
                Id = result
            };
        }
    }
}