using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerCourseDemandList;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Queries
{
    public class WhenHandlingGetEmployerCourseDemandListQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Data_Returned(
            int total,
            GetEmployerCourseDemandListQuery query,
            List<EmployerCourseDemand> listFromService,
            [Frozen] Mock<ICourseDemandService> service,
            GetEmployerCourseDemandListQueryHandler handler)
        {
            //Arrange
            service.Setup(x =>
                    x.GetEmployerCourseDemand(query.Ukprn, query.CourseId, query.Lat, query.Lon, query.Radius))
                .ReturnsAsync(listFromService);
            service.Setup(x => x.GetTotalEmployerCourseDemands(query.Ukprn, query.CourseId)).ReturnsAsync(total);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.CourseDemands.Should().BeEquivalentTo(listFromService);
            actual.Total.Should().Be(total);
        }
    }
}