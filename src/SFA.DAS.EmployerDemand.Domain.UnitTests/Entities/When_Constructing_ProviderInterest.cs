using System;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Entities
{
    public class When_Constructing_ProviderInterest
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Guid id, ProviderInterests source, Guid employerDemandId)
        {
            //Act
            var actual = new Domain.Entities.ProviderInterest(id, source, employerDemandId);
            
            //Assert
            actual.Should().BeEquivalentTo(source, options => options
                .Excluding(interests => interests.EmployerDemandIds));
            actual.EmployerDemandId.Should().Be(employerDemandId);
            actual.Id.Should().Be(id);
        }
    }
}