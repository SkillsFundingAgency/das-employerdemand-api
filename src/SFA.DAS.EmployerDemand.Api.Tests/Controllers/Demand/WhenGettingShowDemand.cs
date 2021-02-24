using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.Controllers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Api.Tests.Controllers.Demand
{
    public class WhenGettingShowDemand
    {
        [Test, MoqAutoData]
        public void Then_Returns_OK(
            [Greedy] DemandController controller)
        {
            var result = controller.ShowDemand() as OkResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}