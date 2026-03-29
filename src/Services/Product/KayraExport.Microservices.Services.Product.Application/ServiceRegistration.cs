using KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions;
using KayraExport.Microservices.Services.Product.Application.Helpers;
using KayraExport.Microservices.Services.Product.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace KayraExport.Microservices.Services.Product.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSharedRebus(x =>
            {
                x.ConnectionString = EnvironmentHelper.RabbitMqConnectionString;
                x.CustomRoutings = [ new() { Type = typeof(ProductCreatedEvent), DestinationAddress = "products-queue" } ];
            });
        }
    }
}
