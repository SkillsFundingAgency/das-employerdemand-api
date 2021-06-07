using System;

namespace SFA.DAS.EmployerDemand.Domain.Models
{
    public class CourseDemandNotificationAudit
    {
        public Guid Id { get; set; }
        public Guid CourseDemandId { get; set; }
    }
}