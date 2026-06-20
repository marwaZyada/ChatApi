using Chat.Domain.Common;
using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Contracts
{
    public interface IAppDbContext
    {
        public DbSet<Message> Messages { get; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
