using System;
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

        public async Task<bool> CreateInterests(Guid id, Domain.Models.ProviderInterests providerInterests)
        {
            var result = false;

            foreach (var employerDemandId in providerInterests.EmployerDemandIds)
            {
                var providerInterest = new Domain.Entities.ProviderInterest(id, providerInterests, employerDemandId);
                result = await _providerInterestRepository.Insert(providerInterest);
            }

            return result;
        }
    }
}