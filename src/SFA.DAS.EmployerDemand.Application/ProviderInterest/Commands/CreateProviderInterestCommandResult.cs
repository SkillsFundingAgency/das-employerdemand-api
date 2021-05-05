using System;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestCommandResult
    {
        public Guid Id { get; set; }
        public bool IsCreated { get; set; }
    }
}