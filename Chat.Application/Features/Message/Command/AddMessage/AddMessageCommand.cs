using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Command.AddMessage
{
    public class AddMessageCommand:IRequest<Unit>
    {
        public string SenderId { get; set; } = string.Empty;

        public string RecepientId { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
       
    }
}
