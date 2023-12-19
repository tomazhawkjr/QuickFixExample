using Microsoft.Extensions.DependencyInjection;
using OrderLibrary.Domain.Interface.Services;
using OrderLibrary.Services;

namespace OrderLibrary.Ioc
{
    public static class ServiceIoc
    {
        public static IServiceCollection AddOrderLibrary(this IServiceCollection services, string configPath)
        {
            services.AddServices(configPath);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, string configPath)
        {
            services.AddSingleton<IQuickFixService>(_ => new QuickFixService(configPath));

            return services;
        }       
    }
}
