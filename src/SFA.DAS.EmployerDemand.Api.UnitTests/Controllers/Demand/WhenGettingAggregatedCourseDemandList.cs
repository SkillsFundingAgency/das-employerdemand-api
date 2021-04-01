using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingAggregatedCourseDemandList
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_List_From_Handler(
            int ukprn,
            int? courseId,
            double? lat,
            double? lon,
            int radius,
            GetAggregatedCourseDemandListResult resultFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetAggregatedCourseDemandListQuery>(query => 
                        query.Ukprn == ukprn
                        && query.CourseId == courseId
                        && query.Lat.Equals(lat)
                        && query.Lon.Equals(lon)
                        && query.Radius == radius),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);

            var result = await controller.GetAggregatedCourseDemandList(ukprn,courseId, lat, lon, radius) as ObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = result.Value as GetAggregatedCourseDemandListResponse;
            model!.AggregatedCourseDemandList.Should().BeEquivalentTo(
                resultFromMediator.AggregatedCourseDemandList.Select(summary =>
                    (GetAggregatedCourseDemandSummaryResponse) summary));
            model.TotalFiltered.Should().Be(resultFromMediator.AggregatedCourseDemandList.Count());
            model.Total.Should().Be(resultFromMediator.Total);
        }
    }
}
