using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingGetEmployerCourseDemandResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(EmployerCourseDemand source)
        {
            //Act
            var actual = (GetEmployerCourseDemandResponse) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=> options
                .Excluding(c=>c.Lat)
                .Excluding(c=>c.Long)
                .Excluding(c=>c.LocationName)
            );

            actual.Location.Name.Should().Be(source.LocationName);
            actual.Location.LocationPoint.GeoPoint.FirstOrDefault().Should().Be(source.Lat);
            actual.Location.LocationPoint.GeoPoint.LastOrDefault().Should().Be(source.Long);
        }
    }
}