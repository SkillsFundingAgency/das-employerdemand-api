using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingGetCourseDemandResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(Domain.Models.CourseDemand source)
        {
            //Act
            var actual = (GetCourseDemandResponse) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=> options
                .Excluding(c=>c.Location.Lat)
                .Excluding(c=>c.Location.Lon)
            );
        }

        [Test]
        public void Then_If_Null_Then_Null_Is_Returned()
        {
            //Act
            var actual = (GetCourseDemandResponse) (Domain.Models.CourseDemand) null;
            
            //Assert
            actual.Should().BeNull();
        }
    }
}