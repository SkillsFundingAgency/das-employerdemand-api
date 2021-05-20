using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingLocationApiResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(Domain.Models.Location source)
        {
            //Act
            var actual = (Location) source;
            
            //Assert
            actual.Name.Should().Be(source.Name);
            actual.LocationPoint.GeoPoint.First().Should().Be(source.Lat);
            actual.LocationPoint.GeoPoint.Last().Should().Be(source.Lon);
        }
    }
}