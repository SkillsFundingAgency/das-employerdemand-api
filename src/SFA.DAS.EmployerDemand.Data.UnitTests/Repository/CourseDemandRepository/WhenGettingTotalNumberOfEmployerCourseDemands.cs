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
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand2.CourseId = courseId;
            courseDemand1.CourseId = courseDemand2.CourseId;
            courseDemand3.CourseId = courseDemand2.CourseId;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3,courseDemand4});
            
            //Act
            var result = await repository.TotalEmployerCourseDemands(ukprn, courseId);
            
            //Assert
            result.Should().Be(3);
        }
    }
}