using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class RebusExtensions
    {
        public static void AddSharedRebus(this IServiceCollection services, Action<RebusConfig> config)
        {
            RebusConfig rc = new();
            config.Invoke(rc);

            services.AddRebus(rebus => rebus
                .Logging(l => l.Console())
                .Transport(t =>
                {
                    if (string.IsNullOrEmpty(rc.InputQueueName))
                        t.UseRabbitMqAsOneWayClient(rc.ConnectionString);
                    else
                        t.UseRabbitMq(rc.ConnectionString, rc.InputQueueName);
                })
                .Routing(r =>
                {
                    var typeBasedRouter = r.TypeBased();

                    typeBasedRouter.Map<LogMessageEvent>("log-queue");

                    if (rc.CustomRoutings != null && rc.CustomRoutings.Any())
                        foreach (var customRouting in rc.CustomRoutings)
                            typeBasedRouter.Map(customRouting.Type, customRouting.DestinationAddress);
                })
            );
        }
    }
}
