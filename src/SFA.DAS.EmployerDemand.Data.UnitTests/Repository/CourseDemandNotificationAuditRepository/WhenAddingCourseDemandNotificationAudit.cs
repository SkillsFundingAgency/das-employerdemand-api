using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandNotificationAuditRepository
{
    public class WhenAddingCourseDemandNotificationAudit
    {
        private Mock<IEmployerDemandDataContext> _employerDemandDataContext;
        private Data.Repository.CourseDemandNotificationAuditRepository _courseDemandNotificationAuditRepository;
        private CourseDemandNotificationAudit _courseDemandNotificationAudit;

        [SetUp]
        public void Arrange()
        {
            _courseDemandNotificationAudit = 
                new CourseDemandNotificationAudit()
                {
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    CourseDemandId = Guid.NewGuid()
                };

            _employerDemandDataContext = new Mock<IEmployerDemandDataContext>();
            _employerDemandDataContext.Setup(x => x.CourseDemandNotificationAudit).ReturnsDbSet(new List<CourseDemandNotificationAudit>());
            _courseDemandNotificationAuditRepository = new Data.Repository.CourseDemandNotificationAuditRepository(Mock.Of<ILogger<Data.Repository.CourseDemandNotificationAuditRepository>>(), _employerDemandDataContext.Object);
        }

        [Test]
        public async Task Then_The_CourseDemand_Item_Is_Added()
        {
            //Act
            await _courseDemandNotificationAuditRepository.Insert(_courseDemandNotificationAudit);
            
            //Assert
            _employerDemandDataContext.Verify(x=>x.CourseDemandNotificationAudit.AddAsync(_courseDemandNotificationAudit, It.IsAny<CancellationToken>()), Times.Once);
            _employerDemandDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}