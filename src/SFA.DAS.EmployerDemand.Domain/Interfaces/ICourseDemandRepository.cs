using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Entities;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandRepository
    {
        Task<bool> Insert(CourseDemand item);
        Task<bool> EmployerDemandsExist(IEnumerable<Guid> idsToCheck);
        Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius, IList<string> routes);
        Task<int> TotalCourseDemands(int ukprn);
        Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandListByCourse(int ukprn, int courseId, double? lat, double? lon, int? radius);
        Task<int> TotalEmployerCourseDemands(int ukprn, int courseId);
        Task<Guid?> VerifyCourseDemandEmail(Guid id);
        Task<CourseDemand> StopCourseDemand(Guid id);
        Task<CourseDemand> GetCourseDemand(Guid id);
        Task<IEnumerable<CourseDemand>> GetCourseDemandsWithNoProviderInterest(uint courseDemandAgeInDays);
        Task<Guid?> UpdateCourseDemand(CourseDemand updateEntity);
        Task<CourseDemand> GetCourseDemandByExpiredId(Guid expiredCourseDemandId);
    }
}