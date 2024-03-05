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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemandByExpiredDemand;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingEmployerDemandByExpiredId
    {
        [Test, MoqAutoData]
        public async Task Then_Mediator_Is_Called_And_Returned_In_Response(
            Guid id,
            GetCourseDemandByExpiredDemandQueryResult result,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandByExpiredDemandQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            
            //Act
            var actual = await controller.GetEmployerCourseDemandByExpiredId(id) as OkObjectResult;
            
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
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandByExpiredDemandQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetCourseDemandByExpiredDemandQueryResult
                {
                    CourseDemand = null
                });
            
            //Act
            var actual = await controller.GetEmployerCourseDemandByExpiredId(id) as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Exception_Returns_Internal_Server_Error(
            Guid id,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetCourseDemandByExpiredDemandQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.GetEmployerCourseDemandByExpiredId(id) as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}