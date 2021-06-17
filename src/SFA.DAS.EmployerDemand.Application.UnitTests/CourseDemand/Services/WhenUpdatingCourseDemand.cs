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
    public class WhenUpdatingCourseDemand
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            Domain.Models.CourseDemand demand,
            Guid id,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Assert
            repository.Setup(
                    x => x.UpdateCourseDemand(
                        It.Is<Domain.Entities.CourseDemand>(c => c.Id.Equals(demand.Id))))
                .ReturnsAsync(id);
            
            //Act
            var actual = await service.UpdateCourseDemand(demand);
            
            //Assert
            actual.Should().Be(id);
        }
        
    }
}