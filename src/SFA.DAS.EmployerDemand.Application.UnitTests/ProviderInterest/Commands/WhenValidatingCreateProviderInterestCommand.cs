using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.ProviderInterest.Commands
{
    public class WhenValidatingCreateProviderInterestCommand
    {
        [Test, AutoData]
        public async Task Then_Command_Is_Valid_If_All_Properties_Supplied(
            CreateProviderInterestsCommand command,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Email = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Command_Is_Valid_If_Required_Properties_Supplied(
            CreateProviderInterestsCommand command,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Email = null;
            command.ProviderInterests.Phone = null;
            command.ProviderInterests.Website = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_EmployerDemandId(
            CreateProviderInterestsCommand command,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.EmployerDemandIds = new List<Guid>();
            command.ProviderInterests.Email = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_Ukprn(
            CreateProviderInterestsCommand command,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Ukprn = int.MinValue;
            command.ProviderInterests.Email = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_Not_Valid_Email(
            CreateProviderInterestsCommand command,
            CreateProviderInterestsCommandValidator validator)
        {
            command.ProviderInterests.Email = "test";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
    }
}