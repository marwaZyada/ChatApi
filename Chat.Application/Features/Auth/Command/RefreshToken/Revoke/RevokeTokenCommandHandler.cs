using Chat.Application.Contracts;
using Chat.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Command.RefreshToken.Revoke
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly IAppDbContext _context;

        public RevokeTokenCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);
            refreshToken.RevokedOnUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
