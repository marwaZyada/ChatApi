using Chat.Application.Contracts;
using Chat.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IAppDbContext _context;

        public GenericRepository(IAppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
          
        }

        public  Task DeleteAsync(T Entity)
        {
           
                _context.Set<T>().Remove(Entity);
            return Task.CompletedTask;

        }

        public  async Task<List<T>> GetAllAsync()
        {
            return await  _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Guid Id)
        {
         return   await _context.Set<T>().FindAsync(Id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public  Task UpdateAsync(T Entity)
        {
           
                _context.Set<T>().Update(Entity);
            return Task.CompletedTask;

        }
    }
}
