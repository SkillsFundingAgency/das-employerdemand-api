using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface IProviderInterestService
    {
        Task<bool> CreateInterest(Models.ProviderInterest providerInterest);
    }
}