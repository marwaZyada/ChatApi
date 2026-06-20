using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateOnly CreatedAt { get; set; }=DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
