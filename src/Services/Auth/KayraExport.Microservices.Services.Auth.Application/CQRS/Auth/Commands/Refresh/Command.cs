using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Refresh
{
    public class Command : CommandBase<Response>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
