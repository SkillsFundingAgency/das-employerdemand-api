using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.EmployerDemand.Domain.Validation.ValidationResult;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingCreateCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called_If_Valid(
            bool result,
            CreateCourseDemandCommand command,
            [Frozen] Mock<IValidator<CreateCourseDemandCommand>> validator,
            [Frozen] Mock<ICourseDemandService> service,
            CreateCourseDemandCommandHandler handler)
        {
            command.CourseDemand.ExpiredCourseDemandId = null;
            validator.Setup(x => x.ValidateAsync(command)).ReturnsAsync(new ValidationResult());
            service.Setup(x=>x.CreateDemand(command.CourseDemand)).ReturnsAsync(result);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            
            validator.Verify(x=>x.ValidateAsync(It.IsAny<CreateCourseDemandCommand>()), Times.Once);
            actual.Id.Should().Be(command.CourseDemand.Id);
            actual.IsCreated.Should().Be(result);
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Record_Is_Not_Created_And_The_ExpiredCourseDemandId_Is_Not_Populated_Then_Id_Returned(
            CreateCourseDemandCommand command,
            [Frozen] Mock<IValidator<CreateCourseDemandCommand>> validator,
            [Frozen] Mock<ICourseDemandService> service,
            CreateCourseDemandCommandHandler handler)
        {
            command.CourseDemand.ExpiredCourseDemandId = null;
            validator.Setup(x => x.ValidateAsync(command)).ReturnsAsync(new ValidationResult());
            service.Setup(x=>x.CreateDemand(command.CourseDemand)).ReturnsAsync(false);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            
            validator.Verify(x=>x.ValidateAsync(It.IsAny<CreateCourseDemandCommand>()), Times.Once);
            actual.Id.Should().Be(command.CourseDemand.Id);
            actual.IsCreated.Should().BeFalse();
        }
        
        [Test, MoqAutoData]
        public async Task Then_If_The_Record_Is_Not_Created_And_The_ExpiredCourseDemandId_Is_Populated_Then_Null_Returned(
            CreateCourseDemandCommand command,
            [Frozen] Mock<IValidator<CreateCourseDemandCommand>> validator,
            [Frozen] Mock<ICourseDemandService> service,
            CreateCourseDemandCommandHandler handler)
        {
            command.CourseDemand.ExpiredCourseDemandId = Guid.NewGuid();
            validator.Setup(x => x.ValidateAsync(command)).ReturnsAsync(new ValidationResult());
            service.Setup(x=>x.CreateDemand(command.CourseDemand)).ReturnsAsync(false);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            
            validator.Verify(x=>x.ValidateAsync(It.IsAny<CreateCourseDemandCommand>()), Times.Once);
            actual.Id.Should().BeNull();
            actual.IsCreated.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Then_If_The_Command_Is_Not_Valid_A_ValidationException_Is_Thrown(
            string propertyName,
            CreateCourseDemandCommand command,
            [Frozen] Mock<IValidator<CreateCourseDemandCommand>> validator,
            [Frozen] Mock<ICourseDemandService> service,
            CreateCourseDemandCommandHandler handler)
        {
            //Arrange
            validator.Setup(x=>x.ValidateAsync(command)).ReturnsAsync(new ValidationResult{ValidationDictionary = { {propertyName,""}}});
            
            //Act
            var act = new Func<Task>(async () => await handler.Handle(command, CancellationToken.None));

            //Assert
            service.Verify(x=>x.CreateDemand(It.IsAny<Domain.Models.CourseDemand>()), Times.Never);
            act.Should().ThrowAsync<ValidationException>()
                .WithMessage($"*{propertyName}*");
        }
    }
}