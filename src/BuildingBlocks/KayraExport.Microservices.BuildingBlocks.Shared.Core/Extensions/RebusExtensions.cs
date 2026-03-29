using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System.Threading.Tasks;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Extensions
{
    public static class RebusExtensions
    {
        public static void AddSharedRebus(this IServiceCollection services, Action<RebusConfig> config)
        {
            RebusConfig rc = new();
            config.Invoke(rc);

            try
            {
                var factory = new ConnectionFactory { Uri = new Uri(rc.ConnectionString) };
                using var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                using var channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

                channel.QueueDeclareAsync(queue: "log-queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
                channel.QueueDeclareAsync(queue: "products-queue", durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();

                if (rc.CustomRoutings != null && rc.CustomRoutings.Any())
                {
                    foreach (var route in rc.CustomRoutings)
                        channel.QueueDeclareAsync(queue: route.DestinationAddress, durable: true, exclusive: false, autoDelete: false, arguments: null).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RabbitMQ'ya ulaşılamadı, kuyruklar oluşturulamadı.");
            }

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
                    {
                        foreach (var customRouting in rc.CustomRoutings)
                            typeBasedRouter.Map(customRouting.Type, customRouting.DestinationAddress);
                    }
                })
            );
        }
    }
}
