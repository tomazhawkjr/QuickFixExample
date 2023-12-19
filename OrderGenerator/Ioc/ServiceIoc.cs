using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderGenerator.Domain.Interfaces.Services;
using OrderGenerator.Infra.Configs;
using OrderGenerator.Infra.Services;
using OrderLibrary.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator.Ioc
{
    public static class ServiceIoc
    {
        public static IServiceCollection AddOrderGenerator(this IServiceCollection services, IConfiguration configuration, string quickFixConfigPath)
        {
            services.AddConfigurations(configuration);
            services.AddOrderLibrary(quickFixConfigPath);
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OrderGeneratorConfig>(configuration.GetSection(nameof(OrderGeneratorConfig)));
            services.AddSingleton(service => service.GetRequiredService<IOptions<OrderGeneratorConfig>>().Value);

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IOrderGeneratorService, OrderGeneratorService>();

            return services;
        }
    }
}
