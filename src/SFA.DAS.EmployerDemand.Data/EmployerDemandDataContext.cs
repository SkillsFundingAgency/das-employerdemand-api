using System;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.EmployerDemand.Data.Configuration;
using SFA.DAS.EmployerDemand.Domain.Configuration;
using CourseDemand = SFA.DAS.EmployerDemand.Data.Configuration.CourseDemand;
using AggregatedCourseDemandSummary = SFA.DAS.EmployerDemand.Data.Configuration.AggregatedCourseDemandSummary;

namespace SFA.DAS.EmployerDemand.Data
{
    public interface IEmployerDemandDataContext
    {
        DbSet<Domain.Entities.CourseDemand> CourseDemands { get; set; }
        DbSet<Domain.Entities.ProviderInterest> ProviderInterests { get; set; }
        DbSet<Domain.Entities.AggregatedCourseDemandSummary> AggregatedCourseDemandSummary { get; set; }
        DbSet<Domain.Entities.CourseDemandNotificationAudit> CourseDemandNotificationAudit { get; set; }
        int SaveChanges();
    }
    
    public class EmployerDemandDataContext : DbContext, IEmployerDemandDataContext
    {
        private const string AzureResource = "https://database.windows.net/";
        private readonly EmployerDemandConfiguration _configuration;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;

        public DbSet<Domain.Entities.CourseDemand> CourseDemands { get; set; }
        public DbSet<Domain.Entities.ProviderInterest> ProviderInterests { get; set; }
        public DbSet<Domain.Entities.AggregatedCourseDemandSummary> AggregatedCourseDemandSummary { get; set; }
        public DbSet<Domain.Entities.CourseDemandNotificationAudit> CourseDemandNotificationAudit { get; set; }

        public EmployerDemandDataContext ()
        {
            
        }
        public EmployerDemandDataContext(DbContextOptions<EmployerDemandDataContext> options) : base(options)
        {
        }
        
        public EmployerDemandDataContext(IOptions<EmployerDemandConfiguration> config, DbContextOptions<EmployerDemandDataContext> options, AzureServiceTokenProvider azureServiceTokenProvider) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration == null || _azureServiceTokenProvider == null)
            {
                return;
            }
            
            var connection = new SqlConnection
            {
                ConnectionString = _configuration.ConnectionString,
                AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result
            };
            optionsBuilder.UseSqlServer(connection);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseDemand());
            modelBuilder.ApplyConfiguration(new ProviderInterest());
            modelBuilder.ApplyConfiguration(new AggregatedCourseDemandSummary());
            modelBuilder.ApplyConfiguration(new CourseDemandNotificationAudit());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}