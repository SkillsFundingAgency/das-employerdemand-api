using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Data.Repository
{
    public class CourseDemandRepository : ICourseDemandRepository
    {
        private readonly ILogger<CourseDemandRepository> _logger;
        private readonly IEmployerDemandDataContext _dataContext;

        public CourseDemandRepository (ILogger<CourseDemandRepository> logger, IEmployerDemandDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }
        public async Task Insert(CourseDemand item)
        {
            try
            {
                await _dataContext.CourseDemands.AddAsync(item);
                _dataContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Unable to add course demand item");
            }
        }
    }
}