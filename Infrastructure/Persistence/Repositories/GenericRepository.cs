using Domain.Contracts;
using Domain.Models.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(ExaminationDbContext context)
        : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntityPrimaryKey<TKey>
    {
        public async Task Add(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        => trackChanges ?
            await context.Set<TEntity>().ToListAsync()
            :
            await context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id) => await context.Set<TEntity>().FindAsync(id);

    }
}
