using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.ProviderInterest.Commands
{
    public class WhenValidatingCreateProviderInterestCommand
    {
        [Test, MoqAutoData]
        public async Task Then_Command_Is_Valid_If_All_Properties_Supplied(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, MoqAutoData]
        public async Task Then_Command_Is_Valid_If_Required_Properties_Supplied(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Website = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, MoqAutoData]
        public async Task Then_Invalid_If_No_EmployerDemandIds(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.EmployerDemandIds = new List<Guid>();
            command.ProviderInterests.Email = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, MoqAutoData]
        public async Task Then_Invalid_If_EmployerDemandId_Not_Exists(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Email = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(false);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, MoqAutoData]
        public async Task Then_Invalid_If_No_Ukprn(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Ukprn = int.MinValue;
            command.ProviderInterests.Email = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, MoqAutoData]
        public async Task Then_Invalid_If_No_Email(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Email = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, MoqAutoData]
        public async Task Then_Invalid_If_No_Phone(
            CreateProviderInterestsCommand command,
            [Frozen] Mock<ICourseDemandService> mockService,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Phone = null;
            mockService
                .Setup(service => service.EmployerDemandsExist(command.ProviderInterests.EmployerDemandIds))
                .ReturnsAsync(true);
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
    }
}