using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class CourseDemandNotificationAudit
    {
        public Guid Id { get; set; }
        public Guid CourseDemandId { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual CourseDemand CourseDemand { get ; set ; }
    }
}