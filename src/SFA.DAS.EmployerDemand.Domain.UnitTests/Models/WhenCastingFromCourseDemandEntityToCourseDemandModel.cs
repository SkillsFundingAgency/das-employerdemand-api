using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingFromCourseDemandEntityToCourseDemandModel
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Mapped(Domain.Entities.CourseDemand source)
        {
            //Act
            var actual = (CourseDemand) source;
            
            //Assert
            actual.Id.Should().Be(source.Id);
            actual.OrganisationName.Should().Be(source.OrganisationName);
            actual.ContactEmailAddress.Should().Be(source.ContactEmailAddress);
            actual.NumberOfApprentices.Should().Be(source.NumberOfApprentices);
            actual.EmailVerified.Should().Be(source.EmailVerified);
            actual.Course.Id.Should().Be(source.CourseId);
            actual.Course.Title.Should().Be(source.CourseTitle);
            actual.Course.Level.Should().Be(source.CourseLevel);
            actual.Course.Route.Should().Be(source.CourseRoute);
            actual.Location.Name.Should().Be(source.LocationName);
            actual.Location.Lat.Should().Be(source.Lat);
            actual.Location.Lon.Should().Be(source.Long);
            actual.StopSharingUrl.Should().Be(source.StopSharingUrl);
        }

        [Test]
        public void Then_If_Null_Then_Null_Returned()
        {
            //Act
            var actual = (CourseDemand) (Domain.Entities.CourseDemand) null;
            
            //Assert
            actual.Should().BeNull();
        }
    }
}