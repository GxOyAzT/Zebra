using Microsoft.Extensions.DependencyInjection;
using Zebra.Shared.LoggerDriver.RabbitMqConfiguration;
using Zebra.Shared.LoggerDriver.RabbitMqConfiguration.Interfaces;
using Zebra.Shared.LoggerDriver.Services;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.Shared.LoggerDriver.DIConfiguration
{
    public static class ConfigureServices
    {
        public static void ConfigureLoggerDriver(this IServiceCollection services)
        {
            services.AddScoped<ICreateModel, CreateModel>();
            services.AddScoped<IMessageLogger, MessageLogger>();
        }

        public static void ConfigureLoggerDriver(this IServiceCollection services, string senderName)
        {
            services.AddScoped<ICreateModel, CreateModel>();
            services.AddScoped<IMessageLogger, MessageLogger>();
            services.AddSingleton(new SenderName(senderName));
        }
    }
}
