using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectManagement.Domain.Repo;
using ProjectManagement.Infrastructure.Persistance.Context;
namespace ProjectManagement.Infrastructure.Repo
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;
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

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
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
