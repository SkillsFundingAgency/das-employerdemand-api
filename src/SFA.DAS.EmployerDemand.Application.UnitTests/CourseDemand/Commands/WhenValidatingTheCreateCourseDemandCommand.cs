using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.CourseDemand.Commands
{
    public class WhenValidatingTheCreateCourseDemandCommand
    {
        [Test, MoqAutoData]
        public async Task Then_Command_Is_Valid_If_All_Properties_Supplied(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_Email(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.ContactEmailAddress = "";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_Not_Valid_Email(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.ContactEmailAddress = "test";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Organisation_Name(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.OrganisationName = "";
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Course(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Course = new Course();
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Course_Id(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Course = new Course
            {
                Level = 1,
                Title = "test",
                Route = "test route"
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Course_Title(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Course = new Course
            {
                Id = 1,
                Level = 1,
                Route = "test route"
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Course_Level(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Course = new Course
            {
                Id = 1,
                Title = "test",
                Route = "test route"
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_Course_Route(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Course = new Course
            {
                Id = 1,
                Title = "test",
                Level = 1
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Location(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Location = new Location();
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Location_Name(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Location = new Location
            {
                Lat = 1,
                Lon = 1
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Location_Lat(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Location = new Location
            {
                Name="test",
                Lat = 0,
                Lon = 1
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_Location_Lon(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.Location = new Location
            {
                Name="test",
                Lat = 1,
                Lon = 0
            };
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_No_StopSharingUrl(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.StopSharingUrl = null;
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
        
        [Test, AutoData]
        public async Task Then_Invalid_If_No_StartSharingUrl(
            CreateCourseDemandCommand command,
            CreateCourseDemandCommandValidator validator)
        {
            command.CourseDemand.StartSharingUrl = null;
            command.CourseDemand.ContactEmailAddress = "test@test.com";
            
            var actual = await validator.ValidateAsync(command);

            actual.IsValid().Should().BeFalse();
        }
    }
}