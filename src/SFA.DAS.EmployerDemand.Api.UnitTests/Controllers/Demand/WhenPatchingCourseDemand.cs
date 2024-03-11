using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenPatchingCourseDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Ok_Returned(
            Guid id,
            PatchCourseDemandCommandResponse response,
            JsonPatchDocument<PatchCourseDemand> request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.Is<PatchCourseDemandCommand>(
                    c=> 
                    c.Id.Equals(id)
                    && c.Patch.Equals(request)
                    ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await controller.PatchDemand(id, request) as OkObjectResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
            actual.Value.Should().BeEquivalentTo((GetCourseDemandResponse) response.CourseDemand );
        }
        
        [Test, MoqAutoData]
        public async Task Then_If_Null_Returned_From_Mediator_Then_NotFound_Is_Returned(
            Guid id,
            JsonPatchDocument<PatchCourseDemand> request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<PatchCourseDemandCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PatchCourseDemandCommandResponse
                {
                    CourseDemand = null
                });
            
            //Act
            var actual = await controller.PatchDemand(id, request) as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }
        
        [Test, MoqAutoData]
        public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
            Guid id,
            JsonPatchDocument<PatchCourseDemand> request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<PatchCourseDemandCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.PatchDemand(id, request) as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}