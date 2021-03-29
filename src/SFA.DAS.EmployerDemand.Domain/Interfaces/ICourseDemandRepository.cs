using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Entities;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandRepository
    {
        Task<bool> Insert(CourseDemand item);
        Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList();
    }
}