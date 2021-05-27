using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Queries.GetUnmetEmployerDemands;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Queries
{
    public class WhenHandlingGetUnmetEmployerDemands
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Ids_Returned_In_The_Response(
            List<Guid> ids,
            GetUnmetEmployerDemandsQuery query,
            [Frozen] Mock<ICourseDemandService> service,
            GetUnmetEmployerDemandsQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetUnmetEmployerDemands(query.AgeOfDemand)).ReturnsAsync(ids);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);

            actual.EmployerDemandIds.Should().BeEquivalentTo(ids);
        }
    }
}