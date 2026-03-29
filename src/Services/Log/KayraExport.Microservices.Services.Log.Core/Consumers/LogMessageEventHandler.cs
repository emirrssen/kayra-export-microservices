using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using Rebus.Handlers;
using Serilog;

namespace KayraExport.Microservices.Services.Log.Core.Consumers;

public class LogMessageEventHandler(ILogger logger) : IHandleMessages<LogMessageEvent>
{
    public async Task Handle(LogMessageEvent message)
    {
        switch (message.LogLevel)
        {
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Trace:
                logger.Debug("{@LogData}", message);
                break;
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Debug:
                logger.Debug("{@LogData}", message);
                break;
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Information:
                logger.Information("{@LogData}", message);
                break;
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Warning:
                logger.Warning("{@LogData}", message);
                break;
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Error:
                logger.Error("{@LogData}", message);
                break;
            case BuildingBlocks.Shared.Domain.Enums.LogLevelEnum.Critical:
                logger.Fatal("{@LogData}", message);
                break;
            default:
                break;
        }

        await Task.CompletedTask;
    }
}
