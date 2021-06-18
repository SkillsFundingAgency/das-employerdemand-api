using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Data.UnitTests.DatabaseMock;
using SFA.DAS.EmployerDemand.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Data.UnitTests.Repository.CourseDemandRepository
{
    public class WhenGettingVerifiedUnmetCourseDemands
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Demands_Are_Returned(
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.CourseId = courseId;
            courseDemand.EmailVerified = true;
            courseDemand.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand.ProviderInterests = new List<ProviderInterest>();
            courseDemand.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>();
            courseDemand.Stopped = false;

            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEquivalentTo(new List<CourseDemand>{courseDemand});
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Unverified_Demands_Not_Returned(
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.CourseId = courseId;
            courseDemand.EmailVerified = false;
            courseDemand.ProviderInterests = new List<ProviderInterest>();
            courseDemand.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>();
            courseDemand.Stopped = false;

            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEmpty();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Demands_With_ProviderInterest_Not_Returned(
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.CourseId = courseId;
            courseDemand.EmailVerified = true;
            courseDemand.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand.ProviderInterests = new List<ProviderInterest>{new ProviderInterest{Id = Guid.NewGuid()}};
            courseDemand.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>();
            courseDemand.Stopped = false;

            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEmpty();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Demands_With_NotificationAudit_Not_Returned(
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.CourseId = courseId;
            courseDemand.EmailVerified = true;
            courseDemand.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand.ProviderInterests = new List<ProviderInterest>();
            courseDemand.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>
            {
                new CourseDemandNotificationAudit
                {
                    Id = Guid.NewGuid(),
                    CourseDemandId = courseDemand.Id,
                    DateCreated = DateTime.UtcNow,
                    CourseDemand = courseDemand
                }
            };
            courseDemand.Stopped = false;

            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEmpty();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Stopped_Demands_Not_Returned(
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand.CourseId = courseId;
            courseDemand.EmailVerified = true;
            courseDemand.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand.ProviderInterests = new List<ProviderInterest>();
            courseDemand.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>();
            courseDemand.Stopped = true;

            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEmpty();
        }

    }
}