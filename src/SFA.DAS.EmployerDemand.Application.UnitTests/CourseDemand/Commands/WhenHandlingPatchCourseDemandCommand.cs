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
            Guid? result,
            PatchCourseDemandCommand command,
            [Frozen] Mock<ICourseDemandService> service,
            PatchCourseDemandCommandHandler handler)
        {
            service.Setup(x=>x.UpdateCourseDemand(command.CourseDemand)).ReturnsAsync(result);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            actual.Id.Should().Be(result);
        }
    }
}