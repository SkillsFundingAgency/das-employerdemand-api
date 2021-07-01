using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetEmployerDemandsOlderThan3Years;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Queries
{
    public class WhenHandlingGetEmployerDemandsOlderThan3Years
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Ids_Returned_In_The_Response(
            List<Domain.Models.CourseDemand> demands,
            GetEmployerDemandsOlderThan3YearsQuery query,
            [Frozen] Mock<ICourseDemandService> mockService,
            GetEmployerDemandsOlderThan3YearsQueryHandler handler)
        {
            //Arrange
            mockService
                .Setup(x => x.GetDemandsOlderThan3Years())
                .ReturnsAsync(demands);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            actual.EmployerDemands.Should().BeEquivalentTo(demands);
        }
    }
}