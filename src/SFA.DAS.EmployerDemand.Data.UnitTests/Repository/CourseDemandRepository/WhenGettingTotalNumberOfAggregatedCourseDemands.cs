using System.Collections.Generic;
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
    public class WhenGettingTotalNumberOfAggregatedCourseDemands
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Total_Is_Returned(
            int ukprn,
            int? courseId,
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand1.CourseId = courseDemand2.CourseId;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3});
            
            //Act
            var result = await repository.TotalCourseDemands(ukprn);
            
            //Assert
            result.Should().Be(2);
        }
    }
}