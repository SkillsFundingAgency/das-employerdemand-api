using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommandHandler : IRequestHandler<CreateProviderInterestCommand, CreateProviderInterestCommandResult> {
        public async Task<CreateProviderInterestCommandResult> Handle(CreateProviderInterestCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}