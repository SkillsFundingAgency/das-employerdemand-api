using System;
using Microsoft.Azure.Services.AppAuthentication;
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
                services.AddSingleton(new AzureServiceTokenProvider());
                services.AddDbContext<EmployerDemandDataContext>(ServiceLifetime.Transient);    
            }
            
            services.AddTransient<IEmployerDemandDataContext, EmployerDemandDataContext>(provider => provider.GetService<EmployerDemandDataContext>());
            services.AddTransient(provider => new Lazy<EmployerDemandDataContext>(provider.GetService<EmployerDemandDataContext>()));
            
        }
    }
}