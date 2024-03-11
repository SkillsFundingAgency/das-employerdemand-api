using System;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EmployerDemand.Data;
using SFA.DAS.EmployerDemand.Domain.Configuration;

namespace SFA.DAS.EmployerDemand.Api.AppStart
{
    public static class AddDatabaseRegistrations
    {
        public static void AddDatabaseRegistration(this IServiceCollection services, EmployerDemandConfiguration config, string environmentName)
        {
            services.AddHttpContextAccessor();
            if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<EmployerDemandDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.EmployerDemand"), ServiceLifetime.Transient);
            }
            else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<EmployerDemandDataContext>(options=>options.UseSqlServer(config.ConnectionString).EnableSensitiveDataLogging(),ServiceLifetime.Transient);
            }
            else
            {
                services.AddDbContext<EmployerDemandDataContext>(ServiceLifetime.Transient);    
            }
            
            services.AddSingleton(new EnvironmentConfiguration(environmentName));
            
            services.AddTransient<IEmployerDemandDataContext, EmployerDemandDataContext>(provider => provider.GetService<EmployerDemandDataContext>());
            services.AddTransient(provider => new Lazy<EmployerDemandDataContext>(provider.GetService<EmployerDemandDataContext>()));
            
            services.AddSingleton(new ChainedTokenCredential(
                new ManagedIdentityCredential(options: new TokenCredentialOptions
                {
                    Retry = { NetworkTimeout = TimeSpan.FromSeconds(1), MaxRetries = 2, Delay = TimeSpan.FromMilliseconds(100) }
                }),
                new AzureCliCredential(options: new AzureCliCredentialOptions()
                {
                    Retry = { NetworkTimeout = TimeSpan.FromSeconds(1), MaxRetries = 2, Delay = TimeSpan.FromMilliseconds(100) }
                }),
                new VisualStudioCodeCredential(options: new VisualStudioCodeCredentialOptions()
                {
                    Retry = { NetworkTimeout = TimeSpan.FromSeconds(1), MaxRetries = 2, Delay = TimeSpan.FromMilliseconds(100) }
                }),
                new VisualStudioCredential(options: new VisualStudioCredentialOptions()
                {
                    Retry = { NetworkTimeout = TimeSpan.FromSeconds(1), MaxRetries = 2, Delay = TimeSpan.FromMilliseconds(100) }
                }))
            );
            
        }
    }
}