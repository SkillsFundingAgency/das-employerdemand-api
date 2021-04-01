using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
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

        public async Task<int> GetAggregatedDemandTotal(int ukprn)
        {
            return await _repository.TotalCourseDemands(ukprn);
        }
        
        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius)
        {
            var summaries = await _repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius);
            return summaries.GroupBy(item => new
                {
                    item.CourseId,
                    item.CourseLevel,
                    item.CourseTitle,
                    item.CourseRoute
                })
                .Select(group =>
                    new AggregatedCourseDemandSummary(
                        group.Key.CourseId, 
                        group.Key.CourseTitle, 
                        group.Key.CourseLevel, 
                        group.Key.CourseRoute, 
                        group.ToList()));
            
        }
    }
}
