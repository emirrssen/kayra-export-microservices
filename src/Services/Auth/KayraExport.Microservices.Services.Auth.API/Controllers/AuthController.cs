using KayraExport.Microservices.BuildingBlocks.Shared.Application.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Commands = KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands;

namespace KayraExport.Microservices.Services.Auth.API.Controllers
{
    [Route("api/auth-service/auth")]
    public class AuthController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Commands.Register.Command command)
            => await ExecuteAsync(command);

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Commands.Login.Command command)
            => await ExecuteAsync(command);

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] Commands.Refresh.Command command)
            => await ExecuteAsync(command);
    }
}
