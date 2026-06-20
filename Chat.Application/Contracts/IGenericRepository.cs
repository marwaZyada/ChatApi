using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetAsync(Guid Id);
        Task AddAsync(T Entity);
        Task UpdateAsync(T Entit);
        Task DeleteAsync(Guid Id);

    }
}
