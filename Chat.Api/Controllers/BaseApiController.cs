using Chat.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected IActionResult Success<T>(T data, string? message = null)
      => Ok(ApiResponse<T>.SuccessResponse(data, message));

        protected IActionResult CreatedResponse<T>(T data, string? message = null)
            => Created(string.Empty, ApiResponse<T>.SuccessResponse(data, message));

        protected IActionResult Fail(string message, List<string>? errors = null)
            => BadRequest(ApiResponse<string>.FailResponse(message, errors));
    }
}
