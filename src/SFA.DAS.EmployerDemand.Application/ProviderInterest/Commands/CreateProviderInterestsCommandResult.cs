using System;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestsCommandResult
    {
        public Guid Id { get; set; }
        public bool IsCreated { get; set; }
    }
}