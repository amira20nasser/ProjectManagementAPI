using ProjectManagement.Domain.Entities;
using System.Linq.Expressions;

namespace ProjectManagement.Application.Abstraction.Repo
{
    public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryAsNoTracking();
        Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
