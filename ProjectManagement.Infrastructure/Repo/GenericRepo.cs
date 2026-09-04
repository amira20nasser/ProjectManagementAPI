using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Persistence.Context;

namespace ProjectManagement.Infrastructure.Repo
{
    public class GenericRepo<TEntity, TKey>(AppDbContext appDbContext) : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
    }
}
