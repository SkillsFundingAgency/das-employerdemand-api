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
    public class WhenGettingAggregatedCourseDemandSummaryList
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_AggregatedCourseDemandList(
            List<CourseDemand> entitiesFromDb,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(entitiesFromDb);
            var expectedAggregatedEntities = entitiesFromDb.GroupBy(demand => demand.CourseId)
                .Select(demands => new AggregatedCourseDemandSummary
                {
                    CourseId = demands.Key,
                    EmployersCount = demands.Select(demand => demand.ContactEmailAddress).Distinct().Count(),
                    ApprenticesCount = demands.Sum(demand => demand.NumberOfApprentices)
                }).OrderBy(summary => summary.CourseTitle);

            //act
            var result = await repository.GetAggregatedCourseDemandSummaryList();

            //assert
            result.Should().BeEquivalentTo(expectedAggregatedEntities);
        }
    }
}
