using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderReceiver.Ioc;

namespace OrderReceiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                   
                    var quickFixConfigurationPath = configuration["OrderReceiverConfig:QuickFixConfigs:ConfigPath"];

                    services.AddOrderReceiver(configuration, quickFixConfigurationPath);
                    services.AddHostedService<OrderReceiverWorker>();
                });
    }
}
