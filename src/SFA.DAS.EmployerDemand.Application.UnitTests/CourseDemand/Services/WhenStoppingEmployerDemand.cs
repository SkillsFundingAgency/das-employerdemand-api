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
    public class WhenStoppingEmployerDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Value_Returned(
            Guid id,
            Guid returnId,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Arrange
            repository
                .Setup(x => x.StopCourseDemand(id))
                .ReturnsAsync(returnId);
            //Act
            var actual = await service.StopCourseDemand(id);
            //Assert
            actual.Should().Be(returnId);
        }
    }
}