using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenVerifyingEmployerDemandEmail
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Value_Returned(
            Guid id,
            Guid returnId,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Arrange
            repository.Setup(x => x.VerifyCourseDemandEmail(id)).ReturnsAsync(returnId);
            //Act
            var actual = await service.VerifyCourseDemandEmail(id);
            //Assert
            actual.Should().Be(returnId);
        }
    }
}
