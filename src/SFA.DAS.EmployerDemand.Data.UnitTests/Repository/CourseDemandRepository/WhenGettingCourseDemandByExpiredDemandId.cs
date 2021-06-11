using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenGettingCourseDemandByExpiredDemandId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Exists_It_Is_Returned_By_Id(
            Guid expiredCourseDemandId,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            courseDemandEntity.ExpiredCourseDemandId = expiredCourseDemandId;
            mockDbContext.Setup(x => x.CourseDemands).ReturnsDbSet(new List<CourseDemand>{courseDemandEntity});
            
            //Act
            var actual = await repository.GetCourseDemandByExpiredId(expiredCourseDemandId);
            
            //Assert
            actual.Should().BeEquivalentTo(courseDemandEntity);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Does_Not_Exist_Null_Returned(
            Guid expiredCourseDemandId,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            mockDbContext.Setup(x => x.CourseDemands).ReturnsDbSet(new List<CourseDemand>{courseDemandEntity});
            
            //Act
            var actual = await repository.GetCourseDemandByExpiredId(expiredCourseDemandId);
            
            //Assert
            actual.Should().BeNull();
        }
    }
}