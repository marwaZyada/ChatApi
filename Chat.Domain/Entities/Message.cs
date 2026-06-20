using Chat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Message:BaseEntity
    {
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public Guid RecepientId { get; set; }
        public string RecepientName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? DateRead { get; set; }
        public DateTime MessageSend { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecepientDeleted { get; set; }

    }
}
