using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandService
    {
        Task<bool> CreateDemand(Models.CourseDemand courseDemand);
        Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius);
        Task<int> GetAggregatedDemandTotal(int ukprn);
    }
}