using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Shared;

namespace Zebra.ProductService.Persistance.Repository.Shared
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
