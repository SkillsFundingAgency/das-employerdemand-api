using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenUpdatingCourseDemand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Exists_Then_It_Is_Updated(
            Guid id,
            string orgName,
            CourseDemand updateEntity,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            courseDemandEntity.Id = id;
            courseDemandEntity.Stopped = false;
            updateEntity.Id = id;
            updateEntity.Stopped = true;
            updateEntity.ContactEmailAddress = null;
            updateEntity.OrganisationName = orgName;
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync(courseDemandEntity);
            
            //Act
            var actual = await repository.UpdateCourseDemand(updateEntity);
            
            //Assert
            mockDbContext.Verify(x => x.SaveChanges(), Times.Once);
            actual.Should().Be(courseDemandEntity.Id);
            courseDemandEntity.Stopped.Should().BeTrue();
            courseDemandEntity.ContactEmailAddress.Should().Be(courseDemandEntity.ContactEmailAddress);
            courseDemandEntity.OrganisationName.Should().Be(orgName);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Does_Not_Exist_Then_Null_Is_Returned(
            CourseDemand demand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(demand.Id))
                .ReturnsAsync((CourseDemand)null);
            
            //Act
            var actual = await repository.UpdateCourseDemand(demand);
            
            //Assert
            mockDbContext.Verify(x => x.CourseDemands.Update(It.IsAny<CourseDemand>()), Times.Never);
            mockDbContext.Verify(x => x.SaveChanges(), Times.Never);
            actual.Should().BeNull();
        }
    }
}