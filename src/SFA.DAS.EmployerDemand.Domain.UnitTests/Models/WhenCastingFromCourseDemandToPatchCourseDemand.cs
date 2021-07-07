using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingFromCourseDemandToPatchCourseDemand
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(CourseDemand source)
        {
            //Act
            var actual = (PatchCourseDemand) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=> options.ExcludingMissingMembers());
        }
    }
}