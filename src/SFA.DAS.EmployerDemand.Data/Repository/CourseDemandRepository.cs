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

        public async Task<CourseDemand> UpdateCourseDemand(CourseDemand updateEntity)
        {
            var courseDemandEntity = await _dataContext.CourseDemands.FindAsync(updateEntity.Id);
            if (courseDemandEntity == null)
            {
                return null;
            }
            
            if (updateEntity.Stopped && !courseDemandEntity.Stopped)
            {
                courseDemandEntity.DateStopped = DateTime.UtcNow;
            }

            if (updateEntity.EmailVerified && !courseDemandEntity.EmailVerified)
            {
                courseDemandEntity.DateEmailVerified = DateTime.UtcNow;
            }
            
            courseDemandEntity.Stopped = updateEntity.Stopped;
            courseDemandEntity.EmailVerified = updateEntity.EmailVerified;
            courseDemandEntity.OrganisationName = updateEntity.OrganisationName ?? courseDemandEntity.OrganisationName;
            courseDemandEntity.ContactEmailAddress = updateEntity.ContactEmailAddress ?? courseDemandEntity.ContactEmailAddress;
            _dataContext.SaveChanges();

            return courseDemandEntity;
        }

        public Task<bool> EmployerDemandsExist(IEnumerable<Guid> idsToCheck)
        {
            return Task.FromResult(idsToCheck.All(id => _dataContext.CourseDemands.Any(c => c.Id == id)));
        }

        public async Task<CourseDemand> StopCourseDemand(Guid id)
        {
            var courseDemandEntity = await _dataContext.CourseDemands.FindAsync(id);
            if (courseDemandEntity == null)
            {
                return null;
            }
            
            courseDemandEntity.Stopped = true;
            courseDemandEntity.DateStopped = DateTime.UtcNow;
            _dataContext.SaveChanges();
            return courseDemandEntity;
        }

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandList(int ukprn, int? courseId, double? lat, double? lon, int? radius, IList<string> routes)
        {
            var result = _dataContext.AggregatedCourseDemandSummary.FromSqlInterpolated(ProviderCourseDemandQuery(ukprn, lat,lon, radius,courseId));
            if (routes?.Count(s => !string.IsNullOrWhiteSpace(s)) > 0)
            {
                result = result.Where(summary => routes.Contains(summary.CourseRoute));
            }
            result = result.OrderBy(summary => summary.CourseTitle);
            
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<AggregatedCourseDemandSummary>> GetAggregatedCourseDemandListByCourse(int ukprn, int courseId, double? lat, double? lon, int? radius)
        {
            return await _dataContext.AggregatedCourseDemandSummary.FromSqlInterpolated(ProviderCourseDemandQueryByCourseId(ukprn, courseId,lat,lon, radius)).ToListAsync();
        }

        public async Task<int> TotalEmployerCourseDemands(int ukprn, int courseId)
        {
            return await _dataContext.CourseDemands
                .GroupJoin(_dataContext.ProviderInterests, 
                    c => new { EmployerDemandId = c.Id, Ukprn = ukprn }, 
                    p => new { p.EmployerDemandId, p.Ukprn }, 
                    (c, p) => new { CourseDemand = c, ProviderInterest = p })
                .SelectMany(combo => combo.ProviderInterest.DefaultIfEmpty(), 
                    (c, p) => new { CourseDemand = c.CourseDemand, ProviderInterest = p })
                .Where(combo => combo.ProviderInterest == null)
                .CountAsync(c => 
                    c.CourseDemand.CourseId.Equals(courseId) 
                    && c.CourseDemand.EmailVerified
                    && !c.CourseDemand.Stopped);
        }

        public async Task<int> TotalCourseDemands(int ukprn)
        {
            var value = await _dataContext.CourseDemands
                .GroupJoin(_dataContext.ProviderInterests, 
                    c => new { EmployerDemandId = c.Id, Ukprn = ukprn }, 
                    p => new { p.EmployerDemandId, p.Ukprn }, 
                    (c, p) => new { CourseDemand = c, ProviderInterest = p })
                .SelectMany(combo => combo.ProviderInterest.DefaultIfEmpty(), 
                    (c, p) => new { CourseDemand = c.CourseDemand, ProviderInterest = p })
                .Where(combo => combo.ProviderInterest == null)
                .Where(c=>c.CourseDemand.EmailVerified)
                .Where(c => !c.CourseDemand.Stopped)
                .GroupBy(c => c.CourseDemand.CourseId)
                .CountAsync();
            
            return value;
        }

        public async Task<CourseDemand> GetCourseDemand(Guid id)
        {
            return await _dataContext.CourseDemands.FindAsync(id);
        }

        public async Task<CourseDemand> GetCourseDemandByExpiredId(Guid expiredCourseDemandId)
        {
            return await _dataContext.CourseDemands.SingleOrDefaultAsync(c => c.ExpiredCourseDemandId.Equals(expiredCourseDemandId));
        }

        public async Task<IEnumerable<CourseDemand>> GetCourseDemandsWithNoProviderInterest(uint courseDemandAgeInDays)
        {
            var courseDemands = await _dataContext.CourseDemands
                .Where(c => c.EmailVerified)
                .Where(c => !c.Stopped)
                .Where(c=>c.DateEmailVerified != null && DateTime.UtcNow > c.DateEmailVerified.Value.AddDays(courseDemandAgeInDays))
                .Where(c => !c.ProviderInterests.Any())
                .Where(c => !c.CourseDemandNotificationAudits.Any(x => x.DateCreated.Date >= x.CourseDemand.DateEmailVerified.Value.AddDays(courseDemandAgeInDays).Date))
                .ToListAsync();

            return courseDemands;
        }

        private FormattableString ProviderCourseDemandQuery(int ukprn, double? lat, double? lon, int? radius, int? courseId)
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
                    left join ProviderInterest pi
                        on pi.EmployerDemandId = c.Id
						and pi.Ukprn = {ukprn}
                    inner join (
                        select
                            cd.CourseId,
                            Sum(NumberOfApprentices) as ApprenticesCount,
                            Count(1) as EmployersCount,
                            Max(dist.DistanceInMiles) as DistanceInMiles
                        from CourseDemand cd
                        left join ProviderInterest pi
							on pi.EmployerDemandId = cd.Id
							and pi.Ukprn = {ukprn}
                    inner join(
                        select
                            Id,
                            courseId,
                            geography::Point(isnull(Lat,0), isnull(Long,0), 4326).STDistance(geography::Point(isnull({lat},0), isnull({lon},0), 4326)) * 0.0006213712 as DistanceInMiles
                        from CourseDemand) as dist on dist.Id = cd.Id and cd.EmailVerified = 1 and cd.stopped = 0 and ({radius} is null or (DistanceInMiles < {radius}))
                    where pi.Ukprn is null
                    Group by cd.CourseId) derv on derv.CourseId = c.CourseId
                    Where ({courseId} is null or c.CourseId = {courseId})
                    and c.EmailVerified = 1
                    and c.Stopped = 0
                    and pi.Ukprn is null";
        }

        private FormattableString ProviderCourseDemandQueryByCourseId(int ukprn, int courseId, double? lat, double? lon, int? radius)
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
                left join ProviderInterest pi
                    on pi.EmployerDemandId = c.Id
					and pi.Ukprn = {ukprn}
                inner join (
                    select
                        Id,
                        courseId,
                        geography::Point(isnull(Lat,0), isnull(Long,0), 4326).STDistance(geography::Point(isnull({lat},0), isnull({lon},0), 4326)) * 0.0006213712 as DistanceInMiles
                    from CourseDemand where EmailVerified=1 and Stopped = 0) as dist on dist.Id = c.Id and ({radius} is null or (DistanceInMiles < {radius}))
                Where c.CourseId = {courseId} 
                and c.EmailVerified = 1
                and c.Stopped = 0
                and pi.Ukprn is null";
        }
    }
}
