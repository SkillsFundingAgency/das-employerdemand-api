using System;
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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.StopCourseDemand;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenPostingStopCourseDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Accepted_Returned(
            Guid id,
            StopCourseDemandCommandResult responseFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<StopCourseDemandCommand>(command => command.Id == id), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseFromMediator);

            //Act
            var actual = await controller.StopEmployerDemand(id) as OkObjectResult;

            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.OK);
            actual.Value.Should().BeEquivalentTo(
                (GetCourseDemandResponse)responseFromMediator.CourseDemand);
        }

        [Test, MoqAutoData]
        public async Task And_Null_Returned_From_Mediator_Then_NotFound_Is_Returned(
            Guid id,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                    It.IsAny<StopCourseDemandCommand>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StopCourseDemandCommandResult());

            //Act
            var actual = await controller.StopEmployerDemand(id) as StatusCodeResult;

            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }

        [Test, MoqAutoData]
        public async Task And_An_Error_Then_An_InternalServer_Error_Is_Returned(
            Guid id,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<StopCourseDemandCommand>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            //Act
            var actual = await controller.StopEmployerDemand(id) as StatusCodeResult;

            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}