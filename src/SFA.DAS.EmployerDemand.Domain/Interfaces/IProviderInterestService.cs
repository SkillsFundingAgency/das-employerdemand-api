using System;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface IProviderInterestService
    {
        Task<bool> CreateInterests(Guid id, Models.ProviderInterests providerInterests);
    }
}