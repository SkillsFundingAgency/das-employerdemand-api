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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingUnmetCourseDemands
    {
        [Test, MoqAutoData]
        public async Task Then_Mediator_Is_Called_And_Data_Returned(
            uint numberOfDays,
            GetUnmetEmployerDemandsQueryResult result,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator
                .Setup(x => x.Send(
                    It.Is<GetUnmetEmployerDemandsQuery>(c=>
                        c.AgeOfDemandInDays.Equals(numberOfDays)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            
            //Act
            var actual = await controller.GetUnmetCourseDemands(numberOfDays) as OkObjectResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
            var actualModel = actual.Value as GetUnmetCourseDemandResponse;
            Assert.That(actualModel, Is.Not.Null);
            actualModel.UnmetCourseDemands.Should()
                .BeEquivalentTo(result.EmployerDemands.Select(c => (GetUnmetCourseDemand)c));
        }

        [Test, MoqAutoData]
        public async Task Then_If_There_Is_An_Exception_Then_Internal_Server_Error_Returned(
            uint numberOfDays,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetUnmetEmployerDemandsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.GetUnmetCourseDemands(numberOfDays) as StatusCodeResult;
            
            //Assert
            Assert.That(actual, Is.Not.Null);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}