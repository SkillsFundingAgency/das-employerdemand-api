using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.StopCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenHandlingStopCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called(
            Domain.Models.CourseDemand resultFromService,
            StopCourseDemandCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            StopCourseDemandCommandHandler handler)
        {
            mockService
                .Setup(x=>x.StopCourseDemand(command.Id))
                .ReturnsAsync(resultFromService);
            
            var actual = await handler.Handle(command, CancellationToken.None);
            
            actual.CourseDemand.Should().BeEquivalentTo(resultFromService);
        }
    }
}