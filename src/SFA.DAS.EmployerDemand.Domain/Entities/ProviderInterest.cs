using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class ProviderInterest
    {
        public Guid Id { get; set; }
        public Guid EmployerDemandId { get; set; }
        public int Ukprn { get; set; }
        public DateTime DateCreated { get; set; }
    }
}