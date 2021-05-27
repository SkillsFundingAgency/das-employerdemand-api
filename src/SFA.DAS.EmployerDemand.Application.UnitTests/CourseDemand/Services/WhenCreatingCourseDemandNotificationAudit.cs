using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenCreatingCourseDemandNotificationAudit
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_With_Mapped_Entity(
            CourseDemandNotificationAudit model,
            [Frozen] Mock<ICourseDemandNotificationAuditRepository> repository,
            CourseDemandNotificationAuditService service)
        {
            //Act
            await service.CreateNotificationAudit(model);
            
            //Assert
            repository.Verify(x=>x.Insert((Domain.Entities.CourseDemandNotificationAudit)model));
        }
    }
}