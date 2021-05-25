using System;
using MediatR;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestsCommand : IRequest<CreateProviderInterestsCommandResult>
    {
        public Guid Id { get ; set ; }
        public Domain.Models.ProviderInterests ProviderInterests { get; set; }
    }
}

