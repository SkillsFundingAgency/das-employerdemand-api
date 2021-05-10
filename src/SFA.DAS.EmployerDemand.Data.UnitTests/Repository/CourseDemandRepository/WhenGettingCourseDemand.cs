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
    public class WhenGettingCourseDemand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_CourseDemand_Is_Returned(
            Guid id,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.Id = id;
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});

            //Act
            var result = await repository.GetCourseDemand(id);
            
            //Assert
            result.Should().Be(courseDemand);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Not_Found_Then_Default_Is_Returned(
            Guid id,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});

            //Act
            var result = await repository.GetCourseDemand(id);
            
            //Assert
            result.Should().BeNull();
        }
    }
}