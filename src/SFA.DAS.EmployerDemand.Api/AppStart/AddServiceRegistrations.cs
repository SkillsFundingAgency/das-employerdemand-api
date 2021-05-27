using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.Api.Common.Interfaces;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Services;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Services;
using SFA.DAS.EmployerDemand.Data.Repository;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Api.AppStart
{
    public static class AddServiceRegistrations
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddTransient<IAzureClientCredentialHelper, AzureClientCredentialHelper>();
            services.AddTransient<ICourseDemandRepository, CourseDemandRepository>();
            services.AddTransient<IProviderInterestRepository, ProviderInterestRepository>();
            services.AddTransient<ICourseDemandNotificationAuditRepository, CourseDemandNotificationAuditRepository>();
                        
            services.AddTransient<ICourseDemandService, CourseDemandService>();
            services.AddTransient<IProviderInterestService, ProviderInterestService>();
        }
    }
}