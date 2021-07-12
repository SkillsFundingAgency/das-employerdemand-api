using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Api.ApiResponses
{
    public class GetDemandsOlderThan3YearsResponse
    {
        public IEnumerable<Guid> EmployerDemandIds { get; set; }
    }
}