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
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            ProviderInterest providerInterest,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand1.CourseId = courseDemand2.CourseId;
            providerInterest.EmployerDemandId = courseDemand3.Id;
            courseDemand1.EmailVerified = true;
            courseDemand2.EmailVerified = false;
            courseDemand3.EmailVerified = false;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3});
            mockDbContext
                .Setup(context => context.ProviderInterests)
                .ReturnsDbSet(new List<ProviderInterest> {providerInterest});
            
            //Act
            var result = await repository.TotalCourseDemands(ukprn);
            
            //Assert
            result.Should().Be(1);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_ProviderInterest_Matches_Ukprn_Then_The_Total_Does_Not_Include_That_CourseDemand(
            int ukprn,
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            ProviderInterest providerInterest,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand1.CourseId = courseDemand2.CourseId;
            providerInterest.EmployerDemandId = courseDemand3.Id;
            providerInterest.Ukprn = ukprn;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3});
            mockDbContext
                .Setup(context => context.ProviderInterests)
                .ReturnsDbSet(new List<ProviderInterest> {providerInterest});
            
            //Act
            var result = await repository.TotalCourseDemands(ukprn);
            
            //Assert
            result.Should().Be(1);
        }
    }
}