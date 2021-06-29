using System.Collections.Generic;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands
{
    public class GetUnmetEmployerDemandsQueryResult 
    {
        public List<Domain.Models.CourseDemand> EmployerDemands { get ; set ; }
    }
}