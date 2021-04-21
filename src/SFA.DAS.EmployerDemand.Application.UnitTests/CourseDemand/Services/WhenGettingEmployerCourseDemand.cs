using System.Collections.Generic;
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
    public class WhenGettingEmployerCourseDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Data_Returned(
            int ukprn,
            int courseId,
            double? lat,
            double? lon,
            int? radius,
            List<Domain.Entities.AggregatedCourseDemandSummary> listFromRepo, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            //Arrange
            mockRepository.Setup(x => x.GetAggregatedCourseDemandListByCourse(ukprn, courseId, lat, lon, radius))
                .ReturnsAsync(listFromRepo);
            
            //Act
            var actual = await service.GetEmployerCourseDemand(ukprn, courseId, lat, lon, radius);

            //Assert
            actual.Should().BeEquivalentTo(listFromRepo, options=> options
                .Excluding(c=>c.CourseId)
                .Excluding(c=>c.CourseTitle)
                .Excluding(c=>c.CourseLevel)
                .Excluding(c=>c.CourseRoute)
                .Excluding(c=>c.EmployersCount)
                .Excluding(c=>c.DistanceInMiles)
            );
        }
    }
}