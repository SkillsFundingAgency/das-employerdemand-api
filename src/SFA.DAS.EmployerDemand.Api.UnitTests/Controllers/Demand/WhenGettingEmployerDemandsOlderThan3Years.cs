using System;
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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerDemandsOlderThan3Years;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingEmployerDemandsOlderThan3Years
    {
        [Test, MoqAutoData]
        public async Task Then_Mediator_Is_Called_And_Data_Returned(
            GetEmployerDemandsOlderThan3YearsResult result,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator
                .Setup(x => x.Send(
                    It.IsAny<GetEmployerDemandsOlderThan3YearsQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            
            //Act
            var actual = await controller.GetDemandsOlderThan3Years() as OkObjectResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
            var actualModel = actual.Value as GetDemandsOlderThan3YearsResponse;
            Assert.That(actualModel, Is.Not.Null);
            actualModel.EmployerDemandIds.Should()
                .BeEquivalentTo(result.EmployerDemands.Select(c => c.Id));
        }

        [Test, MoqAutoData]
        public async Task Then_If_There_Is_An_Exception_Then_Internal_Server_Error_Returned(
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator
                .Setup(x => x.Send(
                    It.IsAny<GetEmployerDemandsOlderThan3YearsQuery>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.GetDemandsOlderThan3Years() as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}