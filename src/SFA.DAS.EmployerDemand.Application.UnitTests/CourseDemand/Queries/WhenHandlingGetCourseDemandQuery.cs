using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Queries
{
    public class WhenHandlingGetCourseDemandQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Data_Returned(
            GetCourseDemandQuery query,
            Domain.Models.CourseDemand result,
            [Frozen] Mock<ICourseDemandService> service,
            GetCourseDemandQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetCourseDemand(query.Id)).ReturnsAsync(result);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.CourseDemand.Should().BeEquivalentTo(result);
        }
    }
}