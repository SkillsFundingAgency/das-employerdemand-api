using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingCourseDemandModelToEntity
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(CourseDemand source)
        {
            //Act
            var actual = (Domain.Entities.CourseDemand)source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options => options
                .Excluding(c=> c.Location)
                .Excluding(c=> c.Course)
            );
            actual.CourseId.Should().Be(source.Course.Id);
            actual.CourseTitle.Should().Be(source.Course.Title);
            actual.CourseLevel.Should().Be(source.Course.Level);
            actual.CourseRoute.Should().Be(source.Course.Route);
            actual.LocationName.Should().Be(source.Location.Name);
            actual.Lat.Should().Be(source.Location.Lat);
            actual.Long.Should().Be(source.Location.Lon);
        }
    }
}