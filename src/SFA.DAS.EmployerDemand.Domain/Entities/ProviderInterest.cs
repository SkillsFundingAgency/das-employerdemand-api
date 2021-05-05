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

        public static implicit operator ProviderInterest(Models.ProviderInterest source)
        {
            return new ProviderInterest
            {
                Id = source.Id,
                EmployerDemandId = source.EmployerDemandId,
                Ukprn = source.Ukprn,
                Email = source.Email,
                Phone = source.Phone,
                Website = source.Website
            };
        }
    }
}