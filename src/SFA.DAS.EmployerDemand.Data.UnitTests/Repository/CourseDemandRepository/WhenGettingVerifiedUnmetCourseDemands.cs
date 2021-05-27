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
        public async Task Then_The_Verified_Demands_Are_Returned_Which_Have_Not_Had_A_Notification_Sent_Or_Any_Interest(
            int ukprn,
            int courseId,
            uint courseDemandAgeInDays,
            CourseDemand courseDemand1,
            CourseDemand courseDemand2,
            CourseDemand courseDemand3,
            CourseDemand courseDemand4,
            ProviderInterest providerInterest,
            [Frozen] Mock<IEmployerDemandDataContext> mockDbContext,
            Data.Repository.CourseDemandRepository repository)
        {
            //arrange
            courseDemand2.CourseId = courseId;
            courseDemand1.CourseId = courseDemand2.CourseId;
            courseDemand3.CourseId = courseDemand2.CourseId;
            
            courseDemand1.EmailVerified = true;
            courseDemand1.DateEmailVerified = DateTime.UtcNow;
            courseDemand1.ProviderInterests = new List<ProviderInterest>();
            
            courseDemand2.EmailVerified = true;
            courseDemand2.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand2.ProviderInterests = new List<ProviderInterest>();
            courseDemand2.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>();
            
            courseDemand3.EmailVerified = true;
            courseDemand3.DateEmailVerified = DateTime.UtcNow.AddDays(-courseDemandAgeInDays--);
            courseDemand3.ProviderInterests = new List<ProviderInterest>();
            courseDemand3.CourseDemandNotificationAudits = new List<CourseDemandNotificationAudit>
            {
                new CourseDemandNotificationAudit
                {
                    Id = Guid.NewGuid(),
                    CourseDemandId = courseDemand3.Id,
                    DateCreated = DateTime.UtcNow.AddDays(-1),
                    CourseDemand = courseDemand3
                }
            };
            
            courseDemand4.EmailVerified = false;
            courseDemand4.DateEmailVerified = null;
            courseDemand4.ProviderInterests = new List<ProviderInterest>();
            
            mockDbContext
                .Setup(context => context.CourseDemands)
                .ReturnsDbSet(new List<CourseDemand>{courseDemand1,courseDemand2,courseDemand3,courseDemand4});
            
            //Act
            var result = await repository.GetCourseDemandsWithNoProviderInterest(courseDemandAgeInDays);
            
            //Assert
            result.Should().BeEquivalentTo(new List<CourseDemand>{courseDemand2});
        }
    }
}