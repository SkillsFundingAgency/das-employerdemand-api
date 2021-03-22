using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands
{
    public class CreateCourseDemandCommandHandler : IRequestHandler<CreateCourseDemandCommand, Guid>
    {
        private readonly ICourseDemandService _service;

        public CreateCourseDemandCommandHandler (ICourseDemandService service)
        {
            _service = service;
        }
        public async Task<Guid> Handle(CreateCourseDemandCommand request, CancellationToken cancellationToken)
        {
            await _service.CreateDemand(request.CourseDemand);
            
            return request.CourseDemand.Id;
        }
    }
}