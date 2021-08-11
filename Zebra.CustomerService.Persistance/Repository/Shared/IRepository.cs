using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.CustomerService.Domain.Shared;

namespace Zebra.CustomerService.Persistance.Repository.Shared
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
