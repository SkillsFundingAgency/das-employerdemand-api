using MediatR;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemandNotificationAudit
{
    public class CreateCourseDemandNotificationAuditCommand : IRequest<Unit>
    {
        public CourseDemandNotificationAudit CourseDemandNotificationAudit { get ; set ; }
    }
}