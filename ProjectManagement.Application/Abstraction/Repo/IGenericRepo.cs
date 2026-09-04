using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Abstraction.Repo
{
    public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
    }
}
