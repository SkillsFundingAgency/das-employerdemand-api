using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandNotificationAuditService
    {
        Task CreateNotificationAudit(CourseDemandNotificationAudit model);
    }
}