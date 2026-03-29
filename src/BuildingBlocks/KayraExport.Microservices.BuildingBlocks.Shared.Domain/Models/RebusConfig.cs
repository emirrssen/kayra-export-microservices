namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models
{
    public class RebusConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string? InputQueueName { get; set; } = null;
    }
}
