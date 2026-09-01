using ProjectManagement.Domain.Entities;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Repo
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        //IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

        System.Threading.Tasks.Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
