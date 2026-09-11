using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace ProjectManagement.Infrastructure.Repo
{
    public class GenericRepo<TEntity, TKey>(AppDbContext appDbContext) : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly DbSet<TEntity> _dbSet = appDbContext.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> QueryAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }
    }
}
