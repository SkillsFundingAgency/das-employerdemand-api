using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class ProviderInterests
    {
        public IEnumerable<Guid> EmployerDemandIds { get; set; }
        public int Ukprn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}