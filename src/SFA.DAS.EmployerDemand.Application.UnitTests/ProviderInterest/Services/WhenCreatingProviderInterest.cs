using System;
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
        public async Task Then_The_Repository_Is_Called_For_Each_CourseDemand(
            Guid id,
            bool returnValue,
            Domain.Models.ProviderInterests providerInterests, 
            [Frozen] Mock<IProviderInterestRepository> mockRepository,
            ProviderInterestService service)
        {
            Domain.Entities.ProviderInterest param = null;
            mockRepository
                .Setup(repository => repository.Insert(
                    It.IsAny<Domain.Entities.ProviderInterest>()))
                .ReturnsAsync(returnValue)
                .Callback((Domain.Entities.ProviderInterest interest) => param = interest);
            
            var actual = await service.CreateInterests(id, providerInterests);

            param!.Should().BeEquivalentTo(providerInterests, options => 
                options
                    .Excluding(interests => interests.EmployerDemandIds)
            );
            actual.Should().Be(returnValue);
            param!.Id.Should().Be(id);
            foreach (var employerDemandId in providerInterests.EmployerDemandIds)
            {
                mockRepository.Verify(repository => repository.Insert(
                    It.Is<Domain.Entities.ProviderInterest>(interest => 
                        interest.EmployerDemandId == employerDemandId)));
            }
        }

        [Test, MoqAutoData]
        public async Task And_Any_Repository_Response_Is_False_Then_Service_Returns_False(
            Guid id,
            bool returnValue,
            Domain.Models.ProviderInterests providerInterests, 
            [Frozen] Mock<IProviderInterestRepository> mockRepository,
            ProviderInterestService service)
        {
            returnValue = false;
            mockRepository
                .Setup(repository => repository.Insert(
                    It.IsAny<Domain.Entities.ProviderInterest>()))
                .ReturnsAsync(returnValue);
            
            var actual = await service.CreateInterests(id, providerInterests);

            actual.Should().Be(returnValue);
        }
    }
}