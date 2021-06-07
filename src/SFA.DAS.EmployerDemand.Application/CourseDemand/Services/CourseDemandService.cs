using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Application.CourseDemand.Services
{
    public class CourseDemandService : ICourseDemandService
    {
        private readonly ICourseDemandRepository _repository;

        public CourseDemandService (ICourseDemandRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateDemand(Domain.Models.CourseDemand courseDemand)
        {
            return await _repository.Insert(courseDemand);
        }

        public async Task<bool> EmployerDemandsExist(IEnumerable<Guid> idsToCheck)
        {
            return await _repository.EmployerDemandsExist(idsToCheck);
        }

        public async Task<int> GetAggregatedDemandTotal(int ukprn)
        {
            return await _repository.TotalCourseDemands(ukprn);
        }
        
        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius, IList<string> routes)
        {
            var summaries = await _repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius, routes);
            return summaries
                .Select(group => (AggregatedCourseDemandSummary) group);
        }

        public async Task<Guid?> VerifyCourseDemandEmail(Guid id)
        {
            return await _repository.VerifyCourseDemandEmail(id);
        }

        public async Task<IEnumerable<EmployerCourseDemand>> GetEmployerCourseDemand(int ukprn, int courseId, double? lat, double? lon, int? radius)
        {
            var summaries = await _repository.GetAggregatedCourseDemandListByCourse(ukprn, courseId, lat, lon, radius);
            var items = summaries
                .Select(group => (EmployerCourseDemand) group);

            return items.OrderBy(c=>c.LocationName);
        }

        public async Task<int> GetTotalEmployerCourseDemands(int ukprn, int courseId)
        {
            return await _repository.TotalEmployerCourseDemands(ukprn, courseId);
        }

        public async Task<Domain.Models.CourseDemand> GetCourseDemand(Guid id)
        {
            return await _repository.GetCourseDemand(id);
        }

        public async Task<IEnumerable<Guid>> GetUnmetEmployerDemands(uint ageOfDemandInDays)
        {
            var demands = await _repository.GetCourseDemandsWithNoProviderInterest(ageOfDemandInDays);

            return demands.Select(c => c.Id);
        }
    }
}
