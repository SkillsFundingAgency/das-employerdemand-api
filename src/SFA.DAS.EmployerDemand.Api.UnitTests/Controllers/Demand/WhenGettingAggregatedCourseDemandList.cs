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
using SFA.DAS.EmployerDemand.Api.ApiModels;
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
            GetAggregatedCourseDemandListForProviderRequest apiRequest,
            GetAggregatedCourseDemandListResult resultFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetAggregatedCourseDemandListQuery>(query => 
                        query.Ukprn == apiRequest.Ukprn),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);

            var result = await controller.GetAggregatedCourseDemandList(apiRequest) as ObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = result.Value as GetAggregatedCourseDemandListResponse;
            model!.AggregatedCourseDemandList.Should().BeEquivalentTo(
                resultFromMediator.AggregatedCourseDemandList.Select(summary =>
                    (GetAggregatedCourseDemandSummaryResponse) summary));
        }
    }
}
