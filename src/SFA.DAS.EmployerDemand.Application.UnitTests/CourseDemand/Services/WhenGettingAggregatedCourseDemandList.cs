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
    public class WhenGettingAggregatedCourseDemandList
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            int ukprn,
            int? courseId,
            double? lat,
            double? lon,
            int? radius,
            List<string> routes,
            List<Domain.Entities.AggregatedCourseDemandSummary> listFromRepo, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            mockRepository
                .Setup(repository => repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius, routes))
                .ReturnsAsync(listFromRepo);

            var result = await service.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius, routes);

            result.Should().BeEquivalentTo(listFromRepo, options => options
                .Excluding(c=>c.DistanceInMiles)
                .Excluding(c=>c.Id)
                .Excluding(c=>c.Lat)
                .Excluding(c=>c.Long)
                .Excluding(c=>c.LocationName)
            );
        }

    }
}