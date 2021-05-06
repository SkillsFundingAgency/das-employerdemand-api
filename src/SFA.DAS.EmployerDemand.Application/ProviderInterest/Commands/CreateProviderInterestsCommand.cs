using MediatR;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestsCommand : IRequest<CreateProviderInterestsCommandResult>
    {
        public Domain.Models.ProviderInterests ProviderInterests { get; set; } 
    }
}

