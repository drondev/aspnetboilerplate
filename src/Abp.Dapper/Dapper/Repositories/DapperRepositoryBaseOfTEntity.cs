using System;
using Abp.Data;
using Abp.Domain.Entities;

namespace Abp.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, Guid>, IDapperRepository<TEntity>
        where TEntity : class, IEntity<Guid>
    {
        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
