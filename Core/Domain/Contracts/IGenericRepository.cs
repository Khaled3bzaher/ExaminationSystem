using Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntityPrimaryKey<TKey>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);
        void Delete(TEntity entity);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity> specifications);

    }
}
