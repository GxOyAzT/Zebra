using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Shared;
using Zebra.ProductService.Persistance.Context;

namespace Zebra.ProductService.Persistance.Repository.Shared
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Get(Guid id) =>
            await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<List<TEntity>> GetAll() =>
            await _dbContext.Set<TEntity>().ToListAsync();

        public virtual async Task Insert(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
