using System;
using System.ComponentModel.DataAnnotations;
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
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.EmployerDemand.Domain.Validation.ValidationResult;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.Demand
{
    public class WhenPostingCreateDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Response_Returned(
            Guid id,
            CreateCourseDemandCommandResponse response,
            CourseDemandRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            response.IsCreated = true;
            request.EntryPoint = EntryPoint.CourseDetail;
            mediator.Setup(x => x.Send(It.Is<CreateCourseDemandCommand>( c=> 
                    c.CourseDemand.Id.Equals(id)
                    && c.CourseDemand.OrganisationName.Equals(request.OrganisationName)
                    && c.CourseDemand.ContactEmailAddress.Equals(request.ContactEmailAddress)
                    && c.CourseDemand.NumberOfApprentices.Equals(request.NumberOfApprentices)
                    && c.CourseDemand.Course.Id.Equals(request.Course.Id)
                    && c.CourseDemand.Course.Title.Equals(request.Course.Title)
                    && c.CourseDemand.Course.Level.Equals(request.Course.Level)
                    && c.CourseDemand.Course.Route.Equals(request.Course.Route)
                    && c.CourseDemand.Location.Name.Equals(request.Location.Name)
                    && c.CourseDemand.Location.Lat == request.Location.LocationPoint.GeoPoint.First()
                    && c.CourseDemand.Location.Lon == request.Location.LocationPoint.GeoPoint.Last()
                    && c.CourseDemand.StopSharingUrl == request.StopSharingUrl
                    && c.CourseDemand.StartSharingUrl == request.StartSharingUrl
                    && c.CourseDemand.ExpiredCourseDemandId == request.ExpiredCourseDemandId
                    && c.CourseDemand.EntryPoint == (short)request.EntryPoint
                    ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await controller.CreateDemand(id, request) as CreatedResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Created);
            actual.Value.Should().BeEquivalentTo(new {response.Id});

        }
        
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Response_Returned_And_If_Not_Created_Accepted_Returned(
            Guid id,
            CreateCourseDemandCommandResponse response,
            CourseDemandRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            response.IsCreated = false;
            mediator.Setup(x => x.Send(It.Is<CreateCourseDemandCommand>( c=> 
                    c.CourseDemand.Id.Equals(id)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await controller.CreateDemand(id, request) as AcceptedResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Accepted);
            actual.Value.Should().BeEquivalentTo(new {response.Id});
        }

        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Response_Returned_And_Conflict_Returned_If_Not_Created_False_And_Id_Is_Null(
            Guid id,
            CreateCourseDemandCommandResponse response,
            CourseDemandRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            response.IsCreated = false;
            response.Id = null;
            mediator.Setup(x => x.Send(It.Is<CreateCourseDemandCommand>( c=> 
                    c.CourseDemand.Id.Equals(id)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await controller.CreateDemand(id, request) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Conflict);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Validation_Exception_Bad_Request_Returned(
            Guid id,
            string errorKey,
            CourseDemandRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            var validationResult = new ValidationResult{ValidationDictionary = { {errorKey,"Some error"}}};
            mediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<CreateCourseDemandCommand>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new ValidationException(validationResult.DataAnnotationResult, null, null));
            
            //Act
            var actual = await controller.CreateDemand(id, request) as ObjectResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            actual.Value.ToString().Should().Contain(errorKey);
            
        }

        [Test, MoqAutoData]
        public async Task Then_If_An_Error_Then_An_InternalServer_Error_Is_Returned(
            Guid id,
            CourseDemandRequest request,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] DemandController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<CreateCourseDemandCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.CreateDemand(id, request) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}