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
    public class WhenGettingTotalNumberOfEmployerCourseDemands
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Total_Is_Returned(
            int ukprn,
            int courseId,
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            CourseDemand courseDemand4,
            ProviderInterest providerInterest,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand2.CourseId = courseId;
            courseDemand1.CourseId = courseDemand2.CourseId;
            courseDemand3.CourseId = courseDemand2.CourseId;
            providerInterest.EmployerDemandId = courseDemand3.Id;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3,courseDemand4});
            mockDbContext
                .Setup(context => context.ProviderInterests)
                .ReturnsDbSet(new List<ProviderInterest> {providerInterest});
            
            //Act
            var result = await repository.TotalEmployerCourseDemands(ukprn, courseId);
            
            //Assert
            result.Should().Be(3);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_ProviderInterest_Matches_Ukprn_Then_The_Total_Does_Not_Include_That_CourseDemand(
            int ukprn,
            int courseId,
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            CourseDemand courseDemand4,
            ProviderInterest providerInterest,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand2.CourseId = courseId;
            courseDemand1.CourseId = courseDemand2.CourseId;
            courseDemand3.CourseId = courseDemand2.CourseId;
            providerInterest.EmployerDemandId = courseDemand3.Id;
            providerInterest.Ukprn = ukprn;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3,courseDemand4});
            mockDbContext
                .Setup(context => context.ProviderInterests)
                .ReturnsDbSet(new List<ProviderInterest> {providerInterest});
            
            //Act
            var result = await repository.TotalEmployerCourseDemands(ukprn, courseId);
            
            //Assert
            result.Should().Be(2);
        }
    }
}