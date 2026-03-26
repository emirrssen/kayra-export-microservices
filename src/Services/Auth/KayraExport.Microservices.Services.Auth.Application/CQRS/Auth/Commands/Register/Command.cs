using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Register
{
    public class Command : CommandBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordRepeat { get; set; } = string.Empty;
    }
}
