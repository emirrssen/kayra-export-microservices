using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Login
{
    public class Command : CommandBase<Response>
    {
        public string LoginCredentials { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
