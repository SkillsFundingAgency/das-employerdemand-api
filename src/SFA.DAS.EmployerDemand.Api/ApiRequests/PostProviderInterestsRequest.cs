using System;
using System.Collections.Generic;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.ApiRequests
{
    public class PostProviderInterestsRequest
    {
        
        public IEnumerable<Guid> EmployerDemandIds { get; set; }
        
        public int Ukprn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public static implicit operator ProviderInterests(PostProviderInterestsRequest source)
        {
            return new ProviderInterests
            {
                EmployerDemandIds = source.EmployerDemandIds,
                Ukprn = source.Ukprn,
                Email = source.Email,
                Phone = source.Phone,
                Website = source.Website
            };
        }
    }
}