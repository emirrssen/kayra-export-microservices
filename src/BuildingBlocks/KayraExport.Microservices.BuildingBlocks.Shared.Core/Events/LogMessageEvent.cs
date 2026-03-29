using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Enums;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Events
{
    public class LogMessageEvent
    {
        public string ServiceName { get; set; } = string.Empty;
        public LogLevelEnum LogLevel { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ExceptionDetails { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
