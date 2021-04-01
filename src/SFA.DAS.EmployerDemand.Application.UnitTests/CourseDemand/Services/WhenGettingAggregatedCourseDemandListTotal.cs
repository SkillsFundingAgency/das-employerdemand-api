using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenGettingAggregatedCourseDemandListTotal
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Total_Returned(
            int totalResults,
            int ukprn,
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            //Arrange
            mockRepository.Setup(x => x.TotalCourseDemands(ukprn)).ReturnsAsync(totalResults);
            
            //Act
            var actual = await service.GetAggregatedDemandTotal(ukprn);
            
            //Assert
            actual.Should().Be(totalResults);
        }
    }
}