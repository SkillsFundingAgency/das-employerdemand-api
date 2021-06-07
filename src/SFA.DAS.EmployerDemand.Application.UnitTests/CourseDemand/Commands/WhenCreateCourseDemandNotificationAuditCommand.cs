using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemandNotificationAudit;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenCreateCourseDemandNotificationAuditCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Command_Is_Handled_And_Service_Called(
            CreateCourseDemandNotificationAuditCommand command,
            [Frozen]Mock<ICourseDemandNotificationAuditService> service,
            CreateCourseDemandNotificationAuditCommandHandler handler)
        {
            //Act
            await handler.Handle(command, CancellationToken.None);
            
            //Assert
            service.Verify(x=>x.CreateNotificationAudit(command.CourseDemandNotificationAudit));
        }
    }
}