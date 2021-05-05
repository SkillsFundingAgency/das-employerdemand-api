using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Services
{
    public class ProviderInterestService : IProviderInterestService
    {
        private readonly IProviderInterestRepository _providerInterestRepository;

        public ProviderInterestService(IProviderInterestRepository providerInterestRepository)
        {
            _providerInterestRepository = providerInterestRepository;
        }

        public async Task<bool> CreateInterest(Domain.Models.ProviderInterest providerInterest)
        {
            return await _providerInterestRepository.Insert(providerInterest);
        }
    }
}