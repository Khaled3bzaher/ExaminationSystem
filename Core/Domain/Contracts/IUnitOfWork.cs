using Domain.Models.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntityPrimaryKey<TKey>;
    }
}
