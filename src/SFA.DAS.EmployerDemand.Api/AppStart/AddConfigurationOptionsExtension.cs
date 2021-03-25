using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.EmployerDemand.Domain.Configuration;

namespace SFA.DAS.EmployerDemand.Api.AppStart
{
    public static class AddConfigurationOptionsExtension
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<EmployerDemandConfiguration>(configuration.GetSection("EmployerDemandConfiguration"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EmployerDemandConfiguration>>().Value);
        }
    }
}