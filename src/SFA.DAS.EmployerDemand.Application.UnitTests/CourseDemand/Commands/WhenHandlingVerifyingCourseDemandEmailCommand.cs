using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.VerifyCourseDemandEmail;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingVerifyingCourseDemandEmailCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called_If_Valid(
            Guid? result,
            VerifyCourseDemandEmailCommand command,
            [Frozen] Mock<ICourseDemandService> service,
            VerifyCourseDemandEmailCommandHandler handler)
        {
            service.Setup(x=>x.VerifyCourseDemandEmail(command.Id)).ReturnsAsync(result);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            actual.Id.Should().Be(result);
        }
    }
}