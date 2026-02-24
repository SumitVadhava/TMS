using TMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TMS.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> DeleteAsync(int id);

        Task<T> UpdateAsync(T entity);

        Task<T?> PatchAsync(int id, Action<T> patchAction);
    }
}