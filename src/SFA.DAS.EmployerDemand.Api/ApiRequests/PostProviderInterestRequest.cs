using System;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.ApiRequests
{
    public class PostProviderInterestRequest
    {
        public Guid Id { get; set; }
        public Guid EmployerDemandId { get; set; }
        public int Ukprn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public static implicit operator ProviderInterest(PostProviderInterestRequest source)
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