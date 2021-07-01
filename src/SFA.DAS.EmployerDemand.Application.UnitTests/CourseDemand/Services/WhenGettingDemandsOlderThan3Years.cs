﻿using System.Collections.Generic;
using System.Linq;
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
    public class WhenGettingDemandsOlderThan3Years
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Old_Demands_Returned(
            List<Domain.Entities.CourseDemand> demands,
            [Frozen] Mock<ICourseDemandRepository> repository,
            CourseDemandService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetDemandsOlderThan3Years())
                .ReturnsAsync(demands);
            
            //Act
            var actual = await service.GetDemandsOlderThan3Years();
            
            //Assert
            actual.Should().BeEquivalentTo(demands.Select(c=>(Domain.Models.CourseDemand)c).ToList());
        }
    }
}