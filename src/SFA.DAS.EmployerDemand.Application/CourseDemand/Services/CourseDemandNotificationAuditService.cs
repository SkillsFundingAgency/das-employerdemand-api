using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Services
{
    public class CourseDemandNotificationAuditService : ICourseDemandNotificationAuditService
    {
        private readonly ICourseDemandNotificationAuditRepository _repository;

        public CourseDemandNotificationAuditService (ICourseDemandNotificationAuditRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateNotificationAudit(CourseDemandNotificationAudit model)
        {
            await _repository.Insert((Domain.Entities.CourseDemandNotificationAudit)model);
        }
    }
}