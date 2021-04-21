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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingEmployerCourseDemandForProvider
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_List_From_Handler(
            int ukprn,
            int courseId,
            double? lat,
            double? lon,
            int radius,
            GetEmployerCourseDemandListResult resultFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetEmployerCourseDemandListQuery>(query => 
                        query.Ukprn == ukprn
                        && query.CourseId == courseId
                        && query.Lat.Equals(lat)
                        && query.Lon.Equals(lon)
                        && query.Radius == radius),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);
            
            //Act
            var result = await controller.GetEmployerCourseDemandByCourse(ukprn,courseId, lat, lon, radius) as ObjectResult;
            
            //Assert
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = result.Value as GetEmployerCourseDemandListResponse;
            model!.EmployerCourseDemands.Should().BeEquivalentTo(
                resultFromMediator.CourseDemands.Select(summary =>
                    (GetEmployerCourseDemandResponse) summary));
            model.TotalFiltered.Should().Be(resultFromMediator.CourseDemands.Count());
            model.Total.Should().Be(resultFromMediator.Total);
        }
    }
}