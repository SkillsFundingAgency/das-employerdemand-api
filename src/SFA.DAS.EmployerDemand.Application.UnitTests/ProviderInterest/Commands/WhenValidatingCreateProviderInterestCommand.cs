using System;
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
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.Email = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Command_Is_Valid_If_Required_Properties_Supplied(
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.Email = null;
            command.ProviderInterest.Phone = null;
            command.ProviderInterest.Website = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_Id(
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.Id = Guid.Empty;
            command.ProviderInterest.Email = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_EmployerDemandId(
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.EmployerDemandId = Guid.Empty;
            command.ProviderInterest.Email = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_Ukprn(
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.Ukprn = int.MinValue;
            command.ProviderInterest.Email = null;
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_Not_Valid_Email(
            CreateProviderInterestCommand command,
            CreateProviderInterestCommandValidator validator)
        {
            command.ProviderInterest.Email = "test";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
    }
}