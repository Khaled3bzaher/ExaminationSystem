using AutoMapper;
using Domain.Contracts;
using Domain.Models.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(ExaminationDbContext context,IMapper mapper) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntityPrimaryKey<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            var repo = new GenericRepository<TEntity, TKey>(context, mapper);
            _repositories.Add(typeName, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
        public async Task<IDbContextTransaction> BeginTransactionAsync() => await context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync() => await context.Database.CommitTransactionAsync();

        public async Task RollbackTransactionAsync() => await context.Database.RollbackTransactionAsync();

        public void Dispose() => context.Dispose();
    }
}
