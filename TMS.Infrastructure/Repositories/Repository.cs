using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Data;
using TMS.Application.Interfaces;
using TMS.Application.Interfaces.Repositories;

namespace TMS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _table;

        public Repository(AppDbContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> PatchAsync(int id, Action<T> patchAction)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                return null;


            patchAction(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task<T?> DeleteAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                return null;

            _table.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}


