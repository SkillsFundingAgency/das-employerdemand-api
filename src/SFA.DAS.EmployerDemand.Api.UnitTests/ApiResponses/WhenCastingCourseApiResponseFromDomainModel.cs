using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingCourseApiResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(Domain.Models.Course source)
        {
            //Act
            var actual = (Course) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source);
        }
    }
}