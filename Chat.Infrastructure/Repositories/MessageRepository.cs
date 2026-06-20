using Chat.Application.Contracts;
using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        

        public MessageRepository(IAppDbContext context): base(context)
        {
           
        }
        public async Task<int> Count()
        {
            return await _context.Messages.CountAsync();
        }
    }
}
