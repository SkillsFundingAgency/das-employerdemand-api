using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Api.AppStart;
using SFA.DAS.EmployerDemand.Domain.Configuration;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Api.UnitTests.AppStart
{
    public class WhenAddingServicesToTheContainer
    {
        [TestCase(typeof(ICourseDemandService))]
        public void Then_The_Dependencies_Are_Correctly_Resolved(Type toResolve)
        {
            var hostEnvironment = new Mock<IWebHostEnvironment>();
            var serviceCollection = new ServiceCollection();
            
            var configuration = GenerateConfiguration();
            var employerDemandConfiguration = configuration
                .GetSection("EmployerDemandConfiguration")
                .Get<EmployerDemandConfiguration>();
            serviceCollection.AddSingleton(hostEnvironment.Object);
            serviceCollection.AddSingleton(Mock.Of<IConfiguration>());
            serviceCollection.AddConfigurationOptions(configuration);
            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddServiceRegistration();
            serviceCollection.AddDatabaseRegistration(employerDemandConfiguration,"DEV");
            serviceCollection.AddLogging();
            
            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService(toResolve);
            Assert.IsNotNull(type);
        }
        
        private static IConfigurationRoot GenerateConfiguration()
        {
            var configSource = new MemoryConfigurationSource
            {
                InitialData = new List<KeyValuePair<string, string>>
                {
                    //new KeyValuePair<string, string>("EmployerDemandConfiguration:ConnectionString", "test"),
                }
            };

            var provider = new MemoryConfigurationProvider(configSource);

            return new ConfigurationRoot(new List<IConfigurationProvider> { provider });
        }
    }
}