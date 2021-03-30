using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiResponses;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiResponses
{
    public class WhenCastingGetAggregatedCourseDemandResponseFromDomainModel
    {
        [Test, AutoData]
        public void Then_Maps_Fields(Domain.Models.AggregatedCourseDemandSummary source)
        {
            var result = (GetAggregatedCourseDemandSummaryResponse)source;

            result.Should().BeEquivalentTo(source);
        }
    }
}