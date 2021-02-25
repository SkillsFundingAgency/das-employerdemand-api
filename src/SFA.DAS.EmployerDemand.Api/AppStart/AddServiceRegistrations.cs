using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.Api.Common.Interfaces;

namespace SFA.DAS.EmployerDemand.Api.AppStart
{
    public static class AddServiceRegistrations
    {
        public static void AddServiceRegistration(this IServiceCollection services, bool isDev)
        {
            services.AddTransient<IAzureClientCredentialHelper, AzureClientCredentialHelper>();
        }
    }
}