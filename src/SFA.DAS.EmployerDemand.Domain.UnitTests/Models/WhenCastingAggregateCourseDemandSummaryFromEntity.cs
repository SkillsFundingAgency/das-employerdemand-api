using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingAggregateCourseDemandSummaryFromEntity
    {
        [Test, AutoData]
        public void Then_Maps_Fields(Entities.AggregatedCourseDemandSummary source)
        {
            var result = (Domain.Models.AggregatedCourseDemandSummary) source;

            result.Should().BeEquivalentTo(source, options=> options.Excluding(c=>c.DistanceInMiles));
        }
    }
}