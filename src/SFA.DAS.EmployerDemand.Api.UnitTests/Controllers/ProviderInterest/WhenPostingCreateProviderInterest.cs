using System;
using System.ComponentModel.DataAnnotations;
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
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.EmployerDemand.Domain.Validation.ValidationResult;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.Controllers.ProviderInterest
{
    public class WhenPostingCreateProviderInterest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Sent_To_Mediator_And_Http_Created_Returned(
            PostProviderInterestsRequest request,
            CreateProviderInterestsCommandResult resultFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            resultFromMediator.IsCreated = true;
            Domain.Models.ProviderInterests model = null;
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<CreateProviderInterestsCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator)
                .Callback((CreateProviderInterestsCommand command, CancellationToken token) => model= command.ProviderInterests);
            
            //Act
            var actual = await controller.CreateProviderInterests(request) as CreatedResult;
            
            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.Created);
            actual!.Value.Should().BeEquivalentTo(new {resultFromMediator.Id});
            model.Should().BeEquivalentTo(request);
        }
        
        [Test, MoqAutoData]
        public async Task And_Not_Created_Then_Http_Accepted_Returned(
            PostProviderInterestsRequest request,
            CreateProviderInterestsCommandResult resultFromMediator,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            resultFromMediator.IsCreated = false;
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<CreateProviderInterestsCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultFromMediator);
            
            //Act
            var actual = await controller.CreateProviderInterests(request) as AcceptedResult;
            
            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.Accepted);
            actual!.Value.Should().BeEquivalentTo(new {resultFromMediator.Id});
        }

        [Test, MoqAutoData]
        public async Task And_ValidationException_Then_Http_BadRequest_Returned(
            string errorKey,
            PostProviderInterestsRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            var validationResult = new ValidationResult{ValidationDictionary = { {errorKey,"Some error"}}};
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<CreateProviderInterestsCommand>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new ValidationException(validationResult.DataAnnotationResult, null, null));
            
            //Act
            var actual = await controller.CreateProviderInterests(request) as ObjectResult;
            
            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            actual!.Value.ToString().Should().Contain(errorKey);
            
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Http_InternalServerError_Returned(
            PostProviderInterestsRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProviderInterestController controller)
        {
            //Arrange
            mockMediator
                .Setup(x => x.Send(
                    It.IsAny<CreateProviderInterestsCommand>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            //Act
            var actual = await controller.CreateProviderInterests(request) as StatusCodeResult;
            
            //Assert
            actual!.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        }
    }
}