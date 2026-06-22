using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Auth.Dto
{
    public record AuthResponse(
    string AccessToken,
    DateTime ExpiresAt);
}
