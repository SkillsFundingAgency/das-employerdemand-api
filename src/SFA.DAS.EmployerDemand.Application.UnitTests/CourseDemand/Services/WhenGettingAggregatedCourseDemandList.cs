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
            int ukprn,
            int? courseId,
            double? lat,
            double? lon,
            int? radius,
            List<Domain.Entities.AggregatedCourseDemandSummary> listFromRepo, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            mockRepository
                .Setup(repository => repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius))
                .ReturnsAsync(listFromRepo);

            var result = await service.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius);

            result.Should().BeEquivalentTo(listFromRepo.Select(summary => (AggregatedCourseDemandSummary)summary), options=>options
                .Excluding(c=>c.Id)
                .Excluding(c=>c.EmployersCount)
            );
        }

        [Test, MoqAutoData]
        public async Task Then_The_Data_Is_Grouped_By_Course_Id(int ukprn,
            int? courseId,
            double? lat,
            double? lon,
            int? radius,
            List<Domain.Entities.AggregatedCourseDemandSummary> listFromRepo, 
            [Frozen] Mock<ICourseDemandRepository> mockRepository,
            CourseDemandService service)
        {
            var repoList = new List<Domain.Entities.AggregatedCourseDemandSummary>();
            repoList.AddRange(listFromRepo);
            repoList.AddRange(listFromRepo);
            mockRepository
                .Setup(repository => repository.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius))
                .ReturnsAsync(repoList);

            var result = (await service.GetAggregatedCourseDemandList(ukprn, courseId, lat, lon, radius)).ToList();

            result.Should().BeEquivalentTo(listFromRepo.Select(summary => (AggregatedCourseDemandSummary)summary), options=>options
                .Excluding(c=>c.Id)
                .Excluding(c=>c.EmployersCount)
                .Excluding(c=>c.ApprenticesCount)
            );

            foreach (var courseDemandSummary in result)
            {
                courseDemandSummary.EmployersCount.Should().Be(2);
                courseDemandSummary.ApprenticesCount.Should().Be(repoList.Where(c=>c.CourseId.Equals(courseDemandSummary.CourseId)).Sum(x=>x.ApprenticesCount));
            }
        }
    }
}