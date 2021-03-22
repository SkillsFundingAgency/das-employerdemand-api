using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingCreateCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called(
            CreateCourseDemandCommand command,
            [Frozen] Mock<ICourseDemandService> service,
            CreateCourseDemandCommandHandler handler)
        {
            var actual = await handler.Handle(command, CancellationToken.None);
            
            service.Verify(x=>x.CreateDemand(command.CourseDemand));
            actual.Should().Be(command.CourseDemand.Id);
        }
    }
}