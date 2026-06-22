using Chat.Application.Features.Auth.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Command.Login
{
    public class LoginCommand:IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
