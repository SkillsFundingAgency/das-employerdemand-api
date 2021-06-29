using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingPatchCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called(
            Domain.Models.CourseDemand result,
            Domain.Models.CourseDemand courseDemand,
            PatchCourseDemandCommand command,
            [Frozen] Mock<ICourseDemandService> service,
            PatchCourseDemandCommandHandler handler)
        {
            //Arrange
            courseDemand.OrganisationName = command.OrganisationName;
            courseDemand.ContactEmailAddress = command.ContactEmailAddress;
            courseDemand.Stopped = command.Stopped;
            service.Setup(x => x.GetCourseDemand(command.Id)).ReturnsAsync(courseDemand);
            service.Setup(x=>x.UpdateCourseDemand(courseDemand)).ReturnsAsync(result);
            
            //Act
            var actual = await handler.Handle(command, CancellationToken.None);
            
            //Assert
            actual.CourseDemand.Should().Be(result);
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Demand_Does_Not_Exist_Null_Returned(
            PatchCourseDemandCommand command,
            [Frozen] Mock<ICourseDemandService> service,
            PatchCourseDemandCommandHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetCourseDemand(command.Id)).ReturnsAsync((Domain.Models.CourseDemand)null);
            
            //Act
            var actual = await handler.Handle(command, CancellationToken.None);
            
            //Assert
            actual.Should().BeEquivalentTo(new PatchCourseDemandCommandResponse());
            service.Verify(x=>x.UpdateCourseDemand(It.IsAny<Domain.Models.CourseDemand>()), Times.Never);
        }
    }
}