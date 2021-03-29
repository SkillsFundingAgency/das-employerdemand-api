using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetAggregatedCourseDemandList;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Queries
{
    public class WhenHandlingGetAggregatedCourseDemandListQuery
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_List_From_Service(
            GetAggregatedCourseDemandListQuery query,
            List<AggregatedCourseDemandSummary> listFromService,
            [Frozen] Mock<ICourseDemandService> mockDemandService,
            GetAggregatedCourseDemandListQueryHandler handler)
        {
            mockDemandService
                .Setup(service => service.GetAggregatedCourseDemandList())
                .ReturnsAsync(listFromService);

            var result = await handler.Handle(query, CancellationToken.None);

            result.AggregatedCourseDemandList.Should().BeEquivalentTo(listFromService);
        }
    }
}