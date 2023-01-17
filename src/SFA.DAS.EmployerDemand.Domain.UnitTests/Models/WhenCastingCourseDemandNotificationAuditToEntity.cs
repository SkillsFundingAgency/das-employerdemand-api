using System;
using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Domain.Models;

namespace SFA.DAS.EmployerDemand.Domain.UnitTests.Models
{
    public class WhenCastingCourseDemandNotificationAuditToEntity
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(CourseDemandNotificationAudit source)
        {
            //Arrange
            source.NotificationType = NotificationType.StoppedCourseClosed;
            
            //Act
            var actual = (Domain.Entities.CourseDemandNotificationAudit) source;
            
            //Assert
            actual.Id.Should().Be(source.Id);
            actual.CourseDemandId.Should().Be(source.CourseDemandId);
            actual.DateCreated.Should().BeCloseTo(DateTime.UtcNow, 1.Seconds());
            actual.NotificationType.Should().Be((short) source.NotificationType);
        }
    }
}