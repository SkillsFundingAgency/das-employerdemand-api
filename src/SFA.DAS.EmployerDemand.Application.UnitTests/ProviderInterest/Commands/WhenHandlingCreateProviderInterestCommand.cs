using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.EmployerDemand.Domain.Validation.ValidationResult;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.ProviderInterest.Commands
{
    public class WhenHandlingCreateProviderInterestCommand
    {
        [Test, MoqAutoData]
        public void And_Command_Not_Valid_Then_ValidationException_Is_Thrown(
            string propertyName,
            CreateProviderInterestCommand command,
            [Frozen] Mock<IValidator<CreateProviderInterestCommand>> mockValidator,
            [Frozen] Mock<IProviderInterestService> mockService,
            CreateProviderInterestCommandHandler handler)
        {
            //Arrange
            mockValidator
                .Setup(x=>x.ValidateAsync(command))
                .ReturnsAsync(new ValidationResult{ValidationDictionary = { {propertyName,""}}});
            
            //Act
            var act = new Func<Task>(async () => await handler.Handle(command, CancellationToken.None));

            //Assert
            act.Should().Throw<ValidationException>()
                .WithMessage($"*{propertyName}*");
            mockService.Verify(x=>x.CreateInterest(It.IsAny<Domain.Models.ProviderInterest>()), Times.Never);
        }

        [Test, MoqAutoData]
        public async Task And_Command_Valid_Then_Service_Called(
            bool result,
            CreateProviderInterestCommand command,
            [Frozen] Mock<IValidator<CreateProviderInterestCommand>> mockValidator,
            [Frozen] Mock<IProviderInterestService> mockService,
            CreateProviderInterestCommandHandler handler)
        {
            mockValidator
                .Setup(x => x.ValidateAsync(command))
                .ReturnsAsync(new ValidationResult());
            mockService
                .Setup(x=>x.CreateInterest(command.ProviderInterest))
                .ReturnsAsync(result);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            
            mockValidator.Verify(x=>x.ValidateAsync(It.IsAny<CreateProviderInterestCommand>()), Times.Once);
            actual.Id.Should().Be(command.ProviderInterest.Id);
            actual.IsCreated.Should().Be(result);
        }
    }
}