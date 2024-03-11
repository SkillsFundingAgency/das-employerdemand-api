using System;
using Azure.Core;
using Azure.Identity;
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
        private readonly ChainedTokenCredential _azureServiceTokenProvider;
        private readonly EnvironmentConfiguration _environmentConfiguration;

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
        
        public EmployerDemandDataContext(IOptions<EmployerDemandConfiguration> config, DbContextOptions<EmployerDemandDataContext> options, ChainedTokenCredential azureServiceTokenProvider,EnvironmentConfiguration environmentConfiguration) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _environmentConfiguration = environmentConfiguration;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration == null 
                || _environmentConfiguration.EnvironmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
                || _environmentConfiguration.EnvironmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            
            var connection = new SqlConnection
            {
                ConnectionString = _configuration.ConnectionString,
                AccessToken = _azureServiceTokenProvider.GetTokenAsync(new TokenRequestContext(scopes: new string[] { AzureResource })).Result.Token,
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