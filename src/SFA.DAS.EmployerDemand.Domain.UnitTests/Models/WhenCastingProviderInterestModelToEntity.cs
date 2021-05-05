using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingProviderInterestModelToEntity
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(ProviderInterest source)
        {
            //Act
            var actual = (Entities.ProviderInterest)source;
            
            //Assert
            actual.Should().BeEquivalentTo(source);
        }
    }
}