using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chat.Application.Features.Auth.Command.Login;
using Chat.Application.Features.Auth.Command.RefreshToken;
using Chat.Application.Features.Auth.Command.RefreshToken.Revoke;
using System.Threading.Tasks;

namespace Chat.Api.Controllers
{
  
    public class AuthsController : BaseApiController
    {
        public AuthsController(IMediator mediator):base(mediator)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Success(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Success(result);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeTokenCommand command)
        {
            await _mediator.Send(command);
            return Success("revoked");
        }
    }
}
