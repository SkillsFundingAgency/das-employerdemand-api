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
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemandNotificationAudit;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenPostingCreateEmployerDemandNotificationAudit
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Response_Returned(
            Guid id,
            Guid courseDemandId,
            NotificationType notificationType,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            notificationType = NotificationType.StoppedCourseClosed;
            
            //Act
            var actual = await controller.CreateDemandNotificationAudit(id, courseDemandId, notificationType) as CreatedResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Created);
            actual.Value.Should().BeEquivalentTo(new {id});

            mediator.Verify(x => x.Send(It.Is<CreateCourseDemandNotificationAuditCommand>( c =>
                c.CourseDemandNotificationAudit.Id.Equals(id)
                && c.CourseDemandNotificationAudit.CourseDemandId.Equals(courseDemandId)
                && c.CourseDemandNotificationAudit.NotificationType.Equals(notificationType)
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test, MoqAutoData]
        public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
            Guid id,
            Guid courseDemandId,
            NotificationType notificationType,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CreateCourseDemandNotificationAuditCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.CreateDemandNotificationAudit(id, courseDemandId, notificationType) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}