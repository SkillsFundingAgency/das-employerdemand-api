using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Data.Repository
{
    public class ProviderInterestRepository : IProviderInterestRepository
    {
        private readonly ILogger<ProviderInterestRepository> _logger;
        private readonly IEmployerDemandDataContext _dataContext;

        public ProviderInterestRepository(ILogger<ProviderInterestRepository> logger, IEmployerDemandDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<bool> Insert(ProviderInterest providerInterest)
        {
            try
            {
                await _dataContext.ProviderInterests.AddAsync(providerInterest);
                _dataContext.SaveChanges();
                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Unable to add provider interest");
            }
            return false;
        }
    }
}