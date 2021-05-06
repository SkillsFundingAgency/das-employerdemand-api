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
            CreateProviderInterestsCommand command,
            [Frozen] Mock<IValidator<CreateProviderInterestsCommand>> mockValidator,
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
            mockService.Verify(x=>x.CreateInterests(It.IsAny<Domain.Models.ProviderInterests>()), Times.Never);
        }

        [Test, MoqAutoData]
        public async Task And_Command_Valid_Then_Service_Called(
            bool result,
            CreateProviderInterestsCommand command,
            [Frozen] Mock<IValidator<CreateProviderInterestsCommand>> mockValidator,
            [Frozen] Mock<IProviderInterestService> mockService,
            CreateProviderInterestCommandHandler handler)
        {
            mockValidator
                .Setup(x => x.ValidateAsync(command))
                .ReturnsAsync(new ValidationResult());
            mockService
                .Setup(x=>x.CreateInterests(command.ProviderInterests))
                .ReturnsAsync(result);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            
            mockValidator.Verify(x=>x.ValidateAsync(It.IsAny<CreateProviderInterestsCommand>()), Times.Once);
            actual.IsCreated.Should().Be(result);
        }
    }
}