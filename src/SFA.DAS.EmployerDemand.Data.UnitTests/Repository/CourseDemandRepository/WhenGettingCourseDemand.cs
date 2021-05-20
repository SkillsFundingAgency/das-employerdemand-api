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
    public class WhenGettingCourseDemand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_If_The_Record_Exists_It_Is_Returned_By_Id(
            Guid id,
            CourseDemand courseDemandEntity,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            mockDbContext.Setup(x => x.CourseDemands.FindAsync(id))
                .ReturnsAsync(courseDemandEntity);
            
            //Act
            var actual = await repository.GetCourseDemand(id);
            
            //Assert
            actual.Should().BeEquivalentTo(courseDemandEntity);
        }
    }
}