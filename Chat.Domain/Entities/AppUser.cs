using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class AppUser:IdentityUser<Guid> 
    {
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Photo>? Photos { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
       = new List<RefreshToken>();
    }
}
