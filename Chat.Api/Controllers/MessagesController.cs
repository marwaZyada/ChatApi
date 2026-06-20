using Chat.Application.Features.Message.Command.AddMessage;
using Chat.Application.Features.Message.Command.AddMessage;
using Chat.Application.Features.Message.Query.GetAllMessage;
using Chat.Application.Features.Message.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chat.Application.Response;

namespace Chat.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        public MessagesController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="command">The command containing message data.</param>
        /// <returns>Action result wrapping the created message or operation result.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMessageCommand command)
        {
            // Send the AddMessageCommand to MediatR to handle message creation.
            var result = await _mediator.Send(command);

            return CreatedResponse(result, "Message created successfully");
        }

        /// <summary>
        /// Retrieves all messages.
        /// </summary>
        /// <returns>List of messages.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllMessageQuery());
            return Success(result);
        }
    }
}
