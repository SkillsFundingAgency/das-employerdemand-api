using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.EmployerDemand.Data.Configuration
{
    public class CourseDemandNotificationAudit : IEntityTypeConfiguration<Domain.Entities.CourseDemandNotificationAudit>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CourseDemandNotificationAudit> builder)
        {
            builder.ToTable("CourseDemandNotificationAudit");
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.CourseDemandId).HasColumnName("CourseDemandId").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").HasColumnType("DateTime").IsRequired();
        }
    }
}