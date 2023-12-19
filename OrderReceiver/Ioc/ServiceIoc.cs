using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderReceiver.Domain.Interfaces.Services;
using OrderReceiver.Infra.Configs;
using OrderReceiver.Infra.Services;
using OrderLibrary.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderReceiver.Ioc
{
    public static class ServiceIoc
    {
        public static IServiceCollection AddOrderReceiver(this IServiceCollection services, IConfiguration configuration, string quickFixConfigPath)
        {
            services.AddConfigurations(configuration);
            services.AddOrderLibrary(quickFixConfigPath);
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OrderReceiverConfig>(configuration.GetSection(nameof(OrderReceiverConfig)));
            services.AddSingleton(service => service.GetRequiredService<IOptions<OrderReceiverConfig>>().Value);

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IOrderReceiverService, OrderReceiverService>();

            return services;
        }
    }
}
