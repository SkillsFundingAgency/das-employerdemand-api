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
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.VerifyCourseDemandEmail;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenVerifyingEmployerDemandEmail
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Accepted_Returned(
            VerifyCourseDemandEmailCommandResponse response,
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<VerifyCourseDemandEmailCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            //Act
            var actual = await controller.VerifyEmployerDemandEmail(id) as AcceptedResult;
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Accepted);
            actual.Value.Should().BeEquivalentTo(new { response.Id });
        }

        [Test, MoqAutoData]
        public async Task Then_If_Null_Returned_From_Mediator_Then_NotFound_Is_Returned(
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<VerifyCourseDemandEmailCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new VerifyCourseDemandEmailCommandResponse
                {
                    Id = null
                });
            //Act
            var actual = await controller.VerifyEmployerDemandEmail(id) as StatusCodeResult;
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }

        [Test, MoqAutoData]
        public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<VerifyCourseDemandEmailCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            //Act
            var actual = await controller.VerifyEmployerDemandEmail(id) as StatusCodeResult;
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}