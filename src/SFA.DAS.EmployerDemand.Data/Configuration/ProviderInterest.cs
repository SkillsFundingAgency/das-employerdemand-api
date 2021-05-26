using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.EmployerDemand.Data.Configuration
{
    public class ProviderInterest: IEntityTypeConfiguration<Domain.Entities.ProviderInterest>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderInterest> builder)
        {
            builder.ToTable("ProviderInterest");
            builder.HasKey(x => new {x.Id, x.EmployerDemandId , x.Ukprn }).HasName("PK_ProviderStandard");
            
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.EmployerDemandId).HasColumnName("EmployerDemandId").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Website).HasColumnName("Website").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime").IsRequired().ValueGeneratedOnAdd();
            
            builder.HasIndex(x => new {x.Id, x.EmployerDemandId , x.Ukprn }).IsUnique();
        }
    }
}