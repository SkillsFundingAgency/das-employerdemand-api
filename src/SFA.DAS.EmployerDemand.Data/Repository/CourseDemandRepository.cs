using System;
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

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius, IList<string> routes)
        {
            var result = _dataContext.AggregatedCourseDemandSummary.FromSqlInterpolated(ProviderCourseDemandQuery(lat,lon, radius,courseId));
            if (routes?.Count(s => !string.IsNullOrWhiteSpace(s)) > 0)
            {
                result = result.Where(summary => routes.Contains(summary.CourseRoute));
            }
            result = result.OrderBy(summary => summary.CourseTitle);
            
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandListByCourse(int ukprn, int courseId, double? lat, double? lon, int? radius)
        {
            return await _dataContext.AggregatedCourseDemandSummary.FromSqlInterpolated(ProviderCourseDemandQueryByCourseId(courseId,lat,lon, radius)).ToListAsync();
        }

        public async Task<int> TotalEmployerCourseDemands(int ukprn, int courseId)
        {
            return await _dataContext.CourseDemands.CountAsync(c => c.CourseId.Equals(courseId));
        }

        public async Task<int> TotalCourseDemands(int ukprn)
        {
            var value = await _dataContext.CourseDemands.GroupBy(c => c.CourseId).CountAsync();
            
            return value;
        }

        private FormattableString ProviderCourseDemandQuery(double? lat, double? lon, int? radius, int? courseId)
        {
            return $@"select distinct
                        null as Id,
                        c.CourseId,
                        c.CourseTitle,
                        c.CourseLevel,
                        c.CourseRoute,
                        derv.ApprenticesCount,
                        derv.EmployersCount,
                        derv.DistanceInMiles,
                        '' as LocationName,
                        null as Lat,
                        null as Long
                    from CourseDemand c
                    inner join (
                        select
                            cd.CourseId,
                            Sum(NumberOfApprentices) as ApprenticesCount,
                            Count(1) as EmployersCount,
                            Max(dist.DistanceInMiles) as DistanceInMiles
                        from CourseDemand cd
                    inner join(
                        select
                            Id,
                            courseId,
                            geography::Point(isnull(Lat,0), isnull(Long,0), 4326).STDistance(geography::Point(isnull({lat},0), isnull({lon},0), 4326)) * 0.0006213712 as DistanceInMiles
                        from CourseDemand) as dist on dist.Id = cd.Id and ({radius} is null or (DistanceInMiles < {radius}))
                    Group by cd.CourseId) derv on derv.CourseId = c.CourseId
                    Where ({courseId} is null or c.CourseId = {courseId})";
        }

        private FormattableString ProviderCourseDemandQueryByCourseId(int courseId, double? lat, double? lon, int? radius)
        {
            return $@"
                select
                    c.Id,
                    c.CourseId,
                    c.CourseTitle,
                    c.CourseLevel,
                    c.CourseRoute,
                    c.LocationName,
                    c.Lat,
                    c.Long,
                    c.NumberOfApprentices as ApprenticesCount,
                    dist.DistanceInMiles,
                    0 as EmployersCount
                from CourseDemand c
                         inner join (
                        select
                            Id,
                            courseId,
                            geography::Point(isnull(Lat,0), isnull(Long,0), 4326).STDistance(geography::Point(isnull({lat},0), isnull({lon},0), 4326)) * 0.0006213712 as DistanceInMiles
                        from CourseDemand) as dist on dist.Id = c.Id and ({radius} is null or (DistanceInMiles < {radius}))
                Where c.CourseId = {courseId}";
        }
    }
}
