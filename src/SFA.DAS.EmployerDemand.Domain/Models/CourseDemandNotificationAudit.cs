using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class CourseDemandNotificationAudit
    {
        public Guid Id { get; set; }
        public Guid CourseDemandId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
    
    public enum NotificationType
    {
        Reminder = 0,
        StoppedByUser = 1,
        StoppedAutomaticCutOff = 2,
        StoppedCourseClosed = 3
    }
}