using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.PatchCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingPatchCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called(
            Domain.Models.CourseDemand result,
            Domain.Models.CourseDemand courseDemand,
            Domain.Models.CourseDemand update,
            Domain.Models.PatchCourseDemand patch,
            [Frozen] Mock<ICourseDemandService> service,
            PatchCourseDemandCommandHandler handler)
        {
            //Arrange
            update = courseDemand;
            patch.Stopped = true;
            patch.EmailVerified = true;
            var patchCommand = new JsonPatchDocument<PatchCourseDemand>();
            patchCommand.Replace(path => path.OrganisationName, patch.OrganisationName);
            patchCommand.Replace(path => path.ContactEmailAddress, patch.ContactEmailAddress);
            patchCommand.Replace(path => path.Stopped, patch.Stopped);
            patchCommand.Replace(path => path.EmailVerified, patch.EmailVerified);
            var command = new PatchCourseDemandCommand
            {
                Id = courseDemand.Id,
                Patch = patchCommand 
            };
            service.Setup(x => x.GetCourseDemand(command.Id)).ReturnsAsync(courseDemand);
            service.Setup(x=>x.UpdateCourseDemand(update)).ReturnsAsync(result);
            
            update.OrganisationName = patch.OrganisationName;
            update.ContactEmailAddress = patch.ContactEmailAddress;
            update.Stopped = patch.Stopped;
            update.EmailVerified = patch.EmailVerified;

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