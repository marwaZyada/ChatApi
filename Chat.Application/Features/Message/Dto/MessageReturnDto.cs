using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Dto
{
    public class MessageReturnDto
    {
        public string SenderName { get; set; } = string.Empty;

        public string RecepientName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public DateTime? DateRead { get; set; }
        
    }
}
