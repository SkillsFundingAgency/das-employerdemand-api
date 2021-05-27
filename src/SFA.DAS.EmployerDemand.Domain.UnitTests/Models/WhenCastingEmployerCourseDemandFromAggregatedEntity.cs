using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingEmployerCourseDemandFromAggregatedEntity
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.AggregatedCourseDemandSummary source)
        {
            //Act
            var actual = (EmployerCourseDemand) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=> options
                .Excluding(c=>c.CourseId)
                .Excluding(c=>c.CourseTitle)
                .Excluding(c=>c.CourseLevel)
                .Excluding(c=>c.CourseRoute)
                .Excluding(c=>c.EmployersCount)
                .Excluding(c=>c.DistanceInMiles)
            );
        }
    }
}