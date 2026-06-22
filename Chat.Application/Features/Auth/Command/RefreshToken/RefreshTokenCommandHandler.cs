using Chat.Application.Contracts;
using Chat.Application.Exceptions;
using Chat.Application.Features.Auth.Dto;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly ICurrentUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenCommandHandler(ICurrentUserService userService, UserManager<AppUser> userManager,
            IJwtService jwtService, IRefreshTokenService refreshTokenService,
             IAppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
           _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user_email = _userService.Email;
            var user =await _userManager.FindByEmailAsync(user_email);
            var refreshToken = user.RefreshTokens
            .FirstOrDefault(x => x.Token == request.RefreshToken);

            if (refreshToken is null)
                throw new UnauthorizedException("refreshToken is null");

            if (refreshToken.IsExpired)
                throw new UnauthorizedException("refreshToken is expired");

            if (refreshToken.IsRevoked)
                throw new UnauthorizedException("refreshToken is revoked");
            if (refreshToken.IsExpired)
                throw new UnauthorizedException("refreshToken is expired");

            refreshToken.RevokedOnUtc = DateTime.UtcNow;

            var accessToken =await _jwtService.GenerateTokenAsync(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();
            var hashRefreshToken = _refreshTokenService.ComputeSha256Hash(newRefreshToken);

            var refresh = new Domain.Entities.RefreshToken
            {
                Token = hashRefreshToken,
                UserId = user.Id,
                CreatedOnUtc = DateTime.UtcNow,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            await _context.RefreshTokens.AddAsync(refresh);
            await _context.SaveChangesAsync(cancellationToken);

            // cookie
            _httpContextAccessor.HttpContext!.Response.Cookies.Append(
                "refreshToken",
                newRefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

            return new AuthResponse(
                accessToken,
                DateTime.UtcNow.AddMinutes(60));



        }
    }
}
