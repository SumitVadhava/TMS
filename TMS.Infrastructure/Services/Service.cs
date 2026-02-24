using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using TMS.Application.Interfaces;
using TMS.Application.Interfaces.Repositories;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Services
{
    public class Service<T> : IService<T>
        where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<T?> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<T> CreateAsync(T entity)
            => (Task<T>)_repository.AddAsync(entity);

        public Task<T> UpdateAsync(T entity)
            => _repository.UpdateAsync(entity);

        public Task<T?> PatchAsync(int id, Action<T> patchAction)
                => _repository.PatchAsync(id, patchAction);



        public Task<T> DeleteAsync(int id)
            => _repository.DeleteAsync(id);
    }
}