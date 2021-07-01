using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.Interfaces
{
    public interface ICourseDemandService
    {
        Task<bool> CreateDemand(Models.CourseDemand courseDemand);
        Task<bool> EmployerDemandsExist(IEnumerable<Guid> idsToCheck);
        Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius, IList<string> routes);
        Task<int> GetAggregatedDemandTotal(int ukprn);
        Task<IEnumerable<EmployerCourseDemand>> GetEmployerCourseDemand(int ukprn, int courseId, double? lat, double? lon, int? radius);
        Task<int> GetTotalEmployerCourseDemands(int ukprn, int courseId);
        Task<Guid?> VerifyCourseDemandEmail(Guid id);
        Task<Domain.Models.CourseDemand> StopCourseDemand(Guid id);
        Task<Domain.Models.CourseDemand> GetCourseDemand(Guid id);
        Task<IEnumerable<CourseDemand>> GetUnmetEmployerDemands(uint ageOfDemandInDays);
        Task<Guid?> UpdateCourseDemand(CourseDemand demand);
        Task<Domain.Models.CourseDemand> GetCourseDemandByExpiredId(Guid expiredCourseDemandId);
        Task<IEnumerable<CourseDemand>> GetDemandsOlderThan3Years();
    }
}