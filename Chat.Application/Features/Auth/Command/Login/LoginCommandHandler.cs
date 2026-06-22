using Chat.Application.Contracts;
using Chat.Application.Exceptions;
using Chat.Application.Features.Auth.Dto;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Chat.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(IJwtService jwtService,UserManager<AppUser> userManager,
          IRefreshTokenService refreshTokenService,SignInManager<AppUser> signInManager,
            IAppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _jwtService = jwtService;
            _userManager = userManager;
           _refreshTokenService = refreshTokenService;
            _signInManager = signInManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null )
            {
                throw new NotFoundException("Invalid email or password.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(
              user,
              request.Password,
              false);

            if (!result.Succeeded)
                throw new NotFoundException("Invalid email or password.");
           
            var accessToken =
      await _jwtService.GenerateTokenAsync(user);

            var refreshToken =
                _refreshTokenService.GenerateRefreshToken();
            var hashRefreshToken = _refreshTokenService.ComputeSha256Hash(refreshToken);
           
           var refresh=  new Domain.Entities.RefreshToken
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
                refreshToken,
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
