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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenGettingUnmetCourseDemands
    {
        [Test, MoqAutoData]
        public async Task Then_Mediator_Is_Called_And_Data_Returned(
            uint numberOfDays,
            int courseId,
            GetUnmetEmployerDemandsQueryResult result,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator
                .Setup(x => x.Send(
                    It.Is<GetUnmetEmployerDemandsQuery>(c=>
                        c.AgeOfDemandInDays.Equals(numberOfDays)
                        && c.CourseId.Equals(courseId)), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            
            //Act
            var actual = await controller.GetUnmetCourseDemands(numberOfDays, courseId) as OkObjectResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.OK);
            actual.Value.Should().BeEquivalentTo(new {result.EmployerDemandIds});
        }

        [Test, MoqAutoData]
        public async Task Then_If_There_Is_An_Exception_Then_Internal_Server_Error_Returned(
            uint numberOfDays,
            int courseId,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetUnmetEmployerDemandsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.GetUnmetCourseDemands(numberOfDays, courseId) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}