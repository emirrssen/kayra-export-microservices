using Rebus.Config;
using Rebus.Routing;
using System.Collections.Generic;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models
{
    public class RebusConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string? InputQueueName { get; set; } = null;
        public IEnumerable<CustomTypeBasedRoutingModel>? CustomRoutings { get; set; } = null!;
    }

    public class CustomTypeBasedRoutingModel
    {
        public Type Type { get; set; } = null!;
        public string DestinationAddress { get; set; } = null!;
    }
}
