using Domain.Models.BaseEntities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntityPrimaryKey<TKey>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity> specifications);
        Task<TDto?> GetProjectedAsync<TDto>(ISpecifications<TEntity> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);
        Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(ISpecifications<TEntity> specifications);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);
        Task<bool> isExists(TKey Id);
        void Update(TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity> specifications);

    }
}
