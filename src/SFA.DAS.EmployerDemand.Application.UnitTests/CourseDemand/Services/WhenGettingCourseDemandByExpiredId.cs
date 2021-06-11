using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenGettingCourseDemandByExpiredId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_CourseDemand_Returned(
            Guid expiredCourseDemandId,
            Domain.Entities.CourseDemand fromRepo,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Arrange
            repository.Setup(x => x.GetCourseDemandByExpiredId(expiredCourseDemandId)).ReturnsAsync(fromRepo);
            
            //Act
            var actual = await service.GetCourseDemandByExpiredId(expiredCourseDemandId);
            
            //Assert
            actual.Id.Should().Be(fromRepo.Id);
        }
    }
}