using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Services
{
    public class WhenGettingAggregatedCourseDemandList
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            List<Domain.Entities.AggregatedCourseDemandSummary> listFromRepo, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            mockRepository
                .Setup(repository => repository.GetAggregatedCourseDemandList())
                .ReturnsAsync(listFromRepo);

            var result = await service.GetAggregatedCourseDemandList();

            result.Should().BeEquivalentTo(listFromRepo.Select(summary => (AggregatedCourseDemandSummary)summary));
        }
    }
}