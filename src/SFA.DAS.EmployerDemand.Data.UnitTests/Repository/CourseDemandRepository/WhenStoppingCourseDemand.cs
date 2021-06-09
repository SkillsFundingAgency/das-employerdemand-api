using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading.Tasks;
using SFA.DAS.EmployerDemand.Domain.Entities;
using FluentAssertions;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenStoppingCourseDemand
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_Demand_Exists_Then_Updated(
            Guid id,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            courseDemandEntity.Id = id;
            courseDemandEntity.DateStopped = null;
            courseDemandEntity.Stopped = false;
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync(courseDemandEntity);
            
            //Act
            var actual = await repository.StopCourseDemand(id);
            
            //Assert
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
            actual.Should().Be(courseDemandEntity.Id);
            courseDemandEntity.Stopped.Should().BeTrue();
            courseDemandEntity.DateStopped.Should().BeCloseTo(DateTime.UtcNow);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Demand_Not_Exist_Then_Null_Returned(
            Guid id,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync((CourseDemand)null);
            
            //Act
            var actual = await repository.StopCourseDemand(id);
            
            //Assert
            mockDbContext.Verify(x => x.CourseDemands.Update(It.IsAny<CourseDemand>()), Times.Never);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Never);
            actual.Should().BeNull();
        }
    }
}