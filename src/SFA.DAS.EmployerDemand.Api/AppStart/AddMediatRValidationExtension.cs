﻿using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EmployerDemand.Application.CourseDemand.Commands.CreateCourseDemand;
using SFA.DAS.EmployerDemand.Application.ProviderInterest.Commands;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Api.AppStart
{
    public static class AddMediatRValidationExtension
    {
        public static void AddMediatRValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateCourseDemandCommand>, CreateCourseDemandCommandValidator>();
            services.AddTransient<IValidator<CreateProviderInterestsCommand>, CreateProviderInterestsCommandValidator>();
        }
    }
}