using MediatR;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommand : IRequest<CreateProviderInterestCommandResult>
    {
        public Domain.Models.ProviderInterest ProviderInterest { get; set; } 
    }
}

