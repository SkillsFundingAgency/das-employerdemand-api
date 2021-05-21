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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemand;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingEmployerDemand
    {
        [Test, MoqAutoData]
        public async Task Then_Mediator_Is_Called_And_Returned_In_Response(
            Guid id,
            GetCourseDemandQueryResult result,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            
            //Act
            var actual = await controller.GetEmployerCourseDemand(id) as OkObjectResult;
            
            //Assert
            actual.Should().NotBeNull();
            var actualModel = actual.Value as GetCourseDemandResponse;
            actualModel.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public async Task Then_If_No_Result_Then_NotFound_Returned(
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetCourseDemandQueryResult
                {
                    CourseDemand = null
                });
            
            //Act
            var actual = await controller.GetEmployerCourseDemand(id) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Exception_Returns_Internal_Server_Error(
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.GetEmployerCourseDemand(id) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}