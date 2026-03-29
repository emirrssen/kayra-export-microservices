using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Auth.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.Services.Auth.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSharedRebus(x =>
            {
                x.ConnectionString = EnvironmentHelper.RabbitMqConnectionString;
            });
        }
    }
}