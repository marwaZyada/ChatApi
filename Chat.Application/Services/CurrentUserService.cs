using Chat.Application.Contracts;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chat.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public Guid? UserId
        {
            get
            {
                var id = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(id, out var guid) ? guid : null;
            }
        }

        public string? Email =>
            User?.FindFirstValue(ClaimTypes.Email);

        public string? UserName =>
            User?.FindFirstValue(ClaimTypes.Name);

        public IEnumerable<string> Roles =>
            User?.FindAll(ClaimTypes.Role).Select(x => x.Value)
            ?? Enumerable.Empty<string>();

       
    }
}
