using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.EmployerDemand.Data.Configuration
{
    public class AggregatedCourseDemandSummary: IEntityTypeConfiguration<Domain.Entities.AggregatedCourseDemandSummary>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AggregatedCourseDemandSummary> builder)
        {
            builder.HasNoKey();
            
            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired(false);
            builder.Property(x => x.CourseId).HasColumnType("int").IsRequired();
            builder.Property(x => x.CourseTitle).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CourseLevel).HasColumnType("int").IsRequired();
            builder.Property(x => x.CourseRoute).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.ApprenticesCount).HasColumnType("int").IsRequired();
            builder.Property(x => x.EmployersCount).HasColumnType("int").IsRequired();
            
            builder.Property(x => x.DistanceInMiles).HasColumnType("float").IsRequired();
            
            builder.Property(x => x.LocationName).HasColumnName("LocationName").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Lat).HasColumnName("Lat").HasColumnType("float").IsRequired(false);
            builder.Property(x => x.Long).HasColumnName("Long").HasColumnType("float").IsRequired(false);

        }
    }
}