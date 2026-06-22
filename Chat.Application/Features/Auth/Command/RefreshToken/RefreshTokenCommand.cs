using Chat.Application.Features.Auth.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Command.RefreshToken
{
    public record RefreshTokenCommand(
    string RefreshToken)
    : IRequest<AuthResponse>;
}
