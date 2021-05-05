using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class ProviderInterest
    {
        public Guid Id { get; set; }
        public Guid EmployerDemandId { get; set; }
        public int Ukprn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public DateTime DateCreated { get; set; }
    }
}