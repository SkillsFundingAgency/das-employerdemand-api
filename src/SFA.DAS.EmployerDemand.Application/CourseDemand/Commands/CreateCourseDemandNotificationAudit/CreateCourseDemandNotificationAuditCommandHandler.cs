using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemandNotificationAudit
{
    public class CreateCourseDemandNotificationAuditCommandHandler : IRequestHandler<CreateCourseDemandNotificationAuditCommand, Unit>
    {
        private readonly ICourseDemandNotificationAuditService _service;

        public CreateCourseDemandNotificationAuditCommandHandler (ICourseDemandNotificationAuditService service)
        {
            _service = service;
        }
        public async Task<Unit> Handle(CreateCourseDemandNotificationAuditCommand request, CancellationToken cancellationToken)
        {
            await _service.CreateNotificationAudit(request.CourseDemandNotificationAudit);
            
            return Unit.Value;
        }
    }
}