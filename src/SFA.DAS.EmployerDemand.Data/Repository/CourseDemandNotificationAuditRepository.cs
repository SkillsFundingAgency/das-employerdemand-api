using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Data.Repository
{
    public class CourseDemandNotificationAuditRepository : ICourseDemandNotificationAuditRepository
    {
        private readonly ILogger<CourseDemandNotificationAuditRepository> _logger;
        private readonly IEmployerDemandDataContext _dataContext;

        public CourseDemandNotificationAuditRepository (ILogger<CourseDemandNotificationAuditRepository> logger, IEmployerDemandDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }
        
        public async Task Insert(CourseDemandNotificationAudit entity)
        {
            try
            {
                await _dataContext.CourseDemandNotificationAudit.AddAsync(entity);
                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, $"Unable to add course demand notification audit record for {entity.CourseDemandId}");
            }
            
        }
    }
}