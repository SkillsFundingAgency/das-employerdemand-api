using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenGettingAggregatedCourseDemandList
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_AggregatedCourseDemandList(
            int ukprn,
            int? courseId,
            double? lat,
            double? lon,
            int? radius,
            List<CourseDemand> entitiesFromDb,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(entitiesFromDb);
            var expectedAggregatedEntities = entitiesFromDb.GroupBy(demand => new
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

            //act
            var result = await repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius);

            //assert
            result.Should().BeEquivalentTo(expectedAggregatedEntities);
        }
    }
}
