using System;

namespace SFA.DAS.EmployerDemand.Domain.Entities
{
    public class CourseDemandNotificationAudit
    {
        public Guid Id { get; set; }
        public Guid CourseDemandId { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual CourseDemand CourseDemand { get ; set ; }

        public static explicit operator CourseDemandNotificationAudit(Models.CourseDemandNotificationAudit source)
        {
            return new CourseDemandNotificationAudit
            {
                Id = source.Id,
                CourseDemandId = source.CourseDemandId,
                DateCreated = DateTime.UtcNow
            };
        }
    }
}