using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.EmployerDemand.Data.Configuration
{
    public class CourseDemand : IEntityTypeConfiguration<Domain.Entities.CourseDemand>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CourseDemand> builder)
        {
            builder.ToTable("CourseDemand");
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ContactEmailAddress).HasColumnName("ContactEmailAddress").HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.OrganisationName).HasColumnName("OrganisationName").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.NumberOfApprentices).HasColumnName("NumberOfApprentices").HasColumnType("int").IsRequired();
            builder.Property(x => x.CourseId).HasColumnName("CourseId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CourseTitle).HasColumnName("CourseTitle").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CourseLevel).HasColumnName("CourseLevel").HasColumnType("int").IsRequired();
            builder.Property(x => x.LocationName).HasColumnName("LocationName").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Lat).HasColumnName("Lat").HasColumnType("float").IsRequired();
            builder.Property(x => x.Long).HasColumnName("Long").HasColumnType("float").IsRequired();
            builder.Property(x => x.EmailVerified).HasColumnName("EmailVerified").HasColumnType("bit").IsRequired();
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.DateEmailVerified).HasColumnName("DateEmailVerified").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.StopSharingUrl).HasColumnName("StopSharingUrl").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.StartSharingUrl).HasColumnName("StartSharingUrl").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Stopped).HasColumnName("Stopped").HasColumnType("bit").IsRequired();
            builder.Property(x => x.DateStopped).HasColumnName("DateStopped").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ExpiredCourseDemandId).HasColumnName("ExpiredCourseDemandId").HasColumnType("uniqueidentifier").IsRequired(false);
            builder.Property(x => x.EntryPoint).HasColumnName("EntryPoint").HasColumnType("smallint").IsRequired(false);
            
            builder.HasIndex(x => x.Id).IsUnique();
            
            builder.HasMany(c => c.ProviderInterests)
                .WithOne(c => c.CourseDemand)
                .HasForeignKey(c => c.EmployerDemandId)
                .HasPrincipalKey(c => c.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
            builder.HasMany(c=>c.CourseDemandNotificationAudits)
                .WithOne(c=>c.CourseDemand)
                .HasForeignKey(c=>c.CourseDemandId)
                .HasPrincipalKey(c=>c.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
            builder.HasOne(c=>c.ExpiredCourseDemand)
                .WithOne()
                .HasForeignKey<Domain.Entities.CourseDemand>(c=>c.ExpiredCourseDemandId)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}