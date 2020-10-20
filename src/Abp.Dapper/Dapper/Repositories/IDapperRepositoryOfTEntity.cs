using System;
using Abp.Domain.Entities;

namespace Abp.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
    }
}
