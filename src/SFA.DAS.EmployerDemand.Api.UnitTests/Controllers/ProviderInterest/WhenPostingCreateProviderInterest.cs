﻿using System;
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
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.EmployerDemand.Domain.Validation.ValidationResult;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.ProviderInterest
{
    public class WhenPostingCreateProviderInterest
    {
        [Test, MoqAutoData, Ignore("todo")]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Http_Created_Returned(
            CreateProviderInterestCommandResult resultFromMediator,
            PostProviderInterestRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            resultFromMediator.IsCreated = true;
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<CreateProviderInterestCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);
            
            //Act
            var actual = await controller.CreateProviderInterest(request) as CreatedResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Created);
            actual.Value.Should().BeEquivalentTo(new {resultFromMediator.Id});

        }
        
        [Test, MoqAutoData, Ignore("todo")]
        public async Task And_Not_Created_Then_Http_Accepted_Returned(
            CreateProviderInterestCommandResult resultFromMediator,
            PostProviderInterestRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            resultFromMediator.IsCreated = false;
            mockMediator
                .Setup(x => x.Send(
                    It.Is<CreateProviderInterestCommand>( c=> 
                    c.ProviderInterest.Id.Equals(request.Id)
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);
            
            //Act
            var actual = await controller.CreateProviderInterest(request) as AcceptedResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Accepted);
            actual.Value.Should().BeEquivalentTo(new {resultFromMediator.Id});
        }

        [Test, MoqAutoData]
        public async Task And_ValidationException_Then_Http_BadRequest_Returned(
            string errorKey,
            PostProviderInterestRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            var validationResult = new ValidationResult{ValidationDictionary = { {errorKey,"Some error"}}};
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<CreateCourseDemandCommand>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new ValidationException(validationResult.DataAnnotationResult, null, null));
            
            //Act
            var actual = await controller.CreateProviderInterest(request) as ObjectResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            actual.Value.ToString().Should().Contain(errorKey);
            
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Http_InternalServerError_Returned(
            PostProviderInterestRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<CreateCourseDemandCommand>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.CreateProviderInterest(request) as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}