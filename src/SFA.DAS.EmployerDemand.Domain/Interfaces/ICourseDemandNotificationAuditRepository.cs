using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Entities;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandNotificationAuditRepository
    {
        Task Insert(CourseDemandNotificationAudit entity);
    }
}