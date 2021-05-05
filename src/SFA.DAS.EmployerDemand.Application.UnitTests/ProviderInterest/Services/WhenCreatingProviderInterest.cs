using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Services;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.ProviderInterest.Services
{
    public class WhenCreatingProviderInterest
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            bool returnValue,
            Domain.Models.ProviderInterest providerInterest, 
            [Frozen] Mock<IProviderInterestRepository> mockRepository,
            ProviderInterestService service)
        {
            Domain.Entities.ProviderInterest param = null;
            mockRepository
                .Setup(repository => repository.Insert(
                    It.IsAny<Domain.Entities.ProviderInterest>()))
                .ReturnsAsync(returnValue)
                .Callback((Domain.Entities.ProviderInterest interest) => param = interest);
            
            var actual = await service.CreateInterest(providerInterest);

            param!.Should().BeEquivalentTo(providerInterest);
            actual.Should().Be(returnValue);
        }
    }
}