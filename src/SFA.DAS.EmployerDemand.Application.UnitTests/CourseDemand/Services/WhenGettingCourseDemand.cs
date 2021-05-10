using System;
using System.Collections.Generic;
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
    public class WhenGettingCourseDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Data_Returned(
            List<Guid> ids,
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            //Arrange
            mockRepository
                .Setup(x => x.EmployerDemandsExist(ids))
                .ReturnsAsync(true);
            
            //Act
            var actual = await service.EmployerDemandsExist(ids);

            //Assert
            actual.Should().BeTrue();
        }
    }
}