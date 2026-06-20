using Chat.Application.Features.Message.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Query.GetAllMessage
{
    public class GetAllMessageQuery:IRequest<List<MessageReturnDto>>
    {
    }
}
