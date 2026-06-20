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
        public string Content { get; set; } = string.Empty;
    }
}
