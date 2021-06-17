using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingGetUnmetCourseDemandResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(Domain.Models.CourseDemand source)
        {
            //Act
            var actual = (GetUnmetCourseDemand) source;
            
            //Assert
            actual.Id.Should().Be(source.Id);
            actual.CourseId.Should().Be(source.Course.Id);
        }
    }
}