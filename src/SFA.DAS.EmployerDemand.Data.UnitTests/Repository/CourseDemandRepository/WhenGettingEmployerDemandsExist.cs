using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenGettingEmployerDemandsExist
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_All_Ids_Exist_Then_Returns_True(
            List<Guid> idsToCheck,
            List<CourseDemand> entities,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            for (var i = 0; i < entities.Count; i++)
            {
                entities[i].Id = idsToCheck[i];
            }
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(entities);

            //Act
            var result = await repository.EmployerDemandsExist(idsToCheck);
            
            //Assert
            result.Should().BeTrue();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Any_Not_Found_Then_Returns_False(
            List<Guid> idsToCheck,
            List<CourseDemand> entities,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            for (var i = 0; i < entities.Count; i++)
            {
                entities[i].Id = idsToCheck[i];
            }
            entities[0].Id = Guid.NewGuid();
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(entities);

            //Act
            var result = await repository.EmployerDemandsExist(idsToCheck);
            
            //Assert
            result.Should().BeFalse();
        }
    }
}