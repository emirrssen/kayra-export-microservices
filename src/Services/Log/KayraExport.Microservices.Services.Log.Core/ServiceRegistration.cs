using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Log.Core.Consumers;
using KayraExport.Microservices.Services.Log.Core.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Serilog;

namespace KayraExport.Microservices.Services.Log.Core
{
    public static class ServiceRegistration
    {
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));
        }

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddRebusHandler<LogMessageEventHandler>();

            services.AddSharedRebus(x =>
            {
                x.InputQueueName = "log-queue";
                x.ConnectionString = EnvironmentHelper.RabbitMqConnectionString;
            });
        }
    }
}
