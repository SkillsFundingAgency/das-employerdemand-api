using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenGettingCourseDemand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Data_Returned(
            Guid id,
            Domain.Entities.CourseDemand courseDemand, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            //Arrange
            mockRepository
                .Setup(x => x.GetCourseDemand(id))
                .ReturnsAsync(courseDemand);
            
            //Act
            var actual = await service.GetDemand(id);

            //Assert
            actual.Should().BeEquivalentTo((EmployerCourseDemand)courseDemand);
        }
    }
}