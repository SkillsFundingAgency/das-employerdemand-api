using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.ApiRequests;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.ApiRequests
{
    public class WhenCastingPostProviderInterestRequestToModel
    {
        [Test, AutoData]
        public void Then_Maps_Fields(PostProviderInterestsRequest source)
        {
            var result = (ProviderInterests)source;

            result.Should().BeEquivalentTo(source);
        }
    }
}