using System.Collections.Generic;
using System.Linq;
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

        [Test, MoqAutoData]
        public async Task Then_The_Locations_Are_Ordered_By_The_District_Name_If_They_Contain_Postcodes_Or_Outcodes(
            int ukprn,
            int courseId,
            double? lat,
            double? lon,
            int? radius,
            Domain.Entities.AggregatedCourseDemandSummary item1, 
            Domain.Entities.AggregatedCourseDemandSummary item2, 
            Domain.Entities.AggregatedCourseDemandSummary item3, 
            Domain.Entities.AggregatedCourseDemandSummary item4, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            //Arrange
            item1.LocationName = "CV1 Somewhere";
            item2.LocationName = "CV1 1QS, Forest";
            item3.LocationName = "Lechlade, Kempsford & Fairford South, Gloucestershire";
            item4.LocationName = "CV2 2QS, Another, Forest";
            var itemsFromRepo = new List<Domain.Entities.AggregatedCourseDemandSummary>
            {
                item1, item2, item3, item4
            };
            mockRepository.Setup(x => x.GetAggregatedCourseDemandListByCourse(ukprn, courseId, lat, lon, radius))
                .ReturnsAsync(itemsFromRepo);

            //Act
            var actual = (await service.GetEmployerCourseDemand(ukprn, courseId, lat, lon, radius)).ToList();

            //Assert
            actual.First().LocationName.Should().Be("Another, Forest, CV2 2QS");
            actual.Skip(1).First().LocationName.Should().Be("Forest, CV1 1QS");
            actual.Skip(2).First().LocationName.Should().Be("Lechlade, Kempsford & Fairford South, Gloucestershire");
            actual.Skip(3).First().LocationName.Should().Be("Somewhere, CV1");
        }
    }
}