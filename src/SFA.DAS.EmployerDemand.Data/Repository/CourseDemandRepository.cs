using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Data.Repository
{
    public class CourseDemandRepository : ICourseDemandRepository
    {
        private readonly ILogger<CourseDemandRepository> _logger;
        private readonly IEmployerDemandDataContext _dataContext;

        public CourseDemandRepository (ILogger<CourseDemandRepository> logger, IEmployerDemandDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<bool> Insert(CourseDemand item)
        {
            try
            {
                await _dataContext.CourseDemands.AddAsync(item);
                _dataContext.SaveChanges();
                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Unable to add course demand item");
            }
            return false;
        }

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius)
        {
            var result = _dataContext.CourseDemands
                .GroupBy(demand => new
                {
                    demand.CourseId, 
                    demand.CourseTitle, 
                    demand.CourseLevel,
                    demand.CourseRoute
                })
                .Select(demands => new AggregatedCourseDemandSummary
                {
                    CourseId = demands.Key.CourseId,
                    CourseTitle = demands.Key.CourseTitle,
                    CourseLevel = demands.Key.CourseLevel,
                    CourseRoute = demands.Key.CourseRoute,
                    EmployersCount = demands.Select(demand => demand.ContactEmailAddress).Distinct().Count(),
                    ApprenticesCount = demands.Sum(demand => demand.NumberOfApprentices)
                }).OrderBy(summary => summary.CourseTitle);

            return await result.ToListAsync();
        }
    }
}
