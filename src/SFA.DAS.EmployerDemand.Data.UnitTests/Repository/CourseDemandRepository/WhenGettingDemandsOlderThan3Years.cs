using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WhenGettingDemandsOlderThan3Years
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Demands_Older_Than_3_Years_Are_Returned(
            List<CourseDemand> courseDemandEntities,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            foreach (var courseDemand in courseDemandEntities)
            {
                courseDemand.DateCreated = DateTime.UtcNow.AddYears(-4);
            }
            mockDbContext.Setup(context => context.CourseDemands)
                .ReturnsDbSet(courseDemandEntities);
            
            //Act
            var actual = await repository.GetDemandsOlderThan3Years();
            
            //Assert
            actual.Should().BeEquivalentTo(courseDemandEntities);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Demands_Younger_Than_3_Years_Are_Not_Returned(
            List<CourseDemand> courseDemandEntities,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            foreach (var courseDemand in courseDemandEntities)
            {
                courseDemand.DateCreated = DateTime.UtcNow.AddYears(-2);
            }
            mockDbContext.Setup(context => context.CourseDemands)
                .ReturnsDbSet(courseDemandEntities);
            
            //Act
            var actual = await repository.GetDemandsOlderThan3Years();
            
            //Assert
            actual.Should().BeEmpty();
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_Anonymised_Demands_Are_Not_Returned(
            List<CourseDemand> courseDemandEntities,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //Arrange
            foreach (var courseDemand in courseDemandEntities)
            {
                courseDemand.DateCreated = DateTime.UtcNow.AddYears(-4);
                courseDemand.ContactEmailAddress = string.Empty;
            }
            mockDbContext.Setup(context => context.CourseDemands)
                .ReturnsDbSet(courseDemandEntities);
            
            //Act
            var actual = await repository.GetDemandsOlderThan3Years();
            
            //Assert
            actual.Should().BeEmpty();
        }
    }
}