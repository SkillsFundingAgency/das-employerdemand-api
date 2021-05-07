using System;

namespace SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands
{
    public class CreateProviderInterestsCommandResult
    {
        public int Ukprn { get; set; }
        public bool IsCreated { get; set; }
    }
}