using AutoMapper;
using Chat.Application.Features.Message.Dto;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.MappingProfile
{
    public class MessageMapping:Profile
    {
        public MessageMapping()
        {
            CreateMap<Message, MessageReturnDto>().ReverseMap();
                //.ForMember(x=>x.SenderName,opt=>opt.MapFrom(x=>x.));
        }
    }
}
