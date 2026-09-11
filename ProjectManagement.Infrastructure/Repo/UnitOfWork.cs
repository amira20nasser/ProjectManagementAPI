using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Persistence.Context;

namespace ProjectManagement.Infrastructure.Repo
{
    public class UnitOfWork(AppDbContext dbContext, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;
        public IGenericRepo<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return serviceProvider.GetRequiredService<IGenericRepo<TEntity, TKey>>();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
           if(_currentTransaction is not null)
                throw new InvalidOperationException("A transaction is already in progress.");
            _currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("No transaction is currently in progress.");

            try
            {
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await DisposeCurrentTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null)
                return;

            try
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await DisposeCurrentTransactionAsync();
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DisposeCurrentTransactionAsync()
        {
            if (_currentTransaction is null)
                return;

            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
}
