using Chat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresOnUtc { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? RevokedOnUtc { get; set; }

        public bool IsRevoked => RevokedOnUtc.HasValue;

        public bool IsExpired => DateTime.UtcNow >= ExpiresOnUtc;

        public Guid UserId { get; set; }

        public AppUser User { get; set; } = default!;
    }
}
