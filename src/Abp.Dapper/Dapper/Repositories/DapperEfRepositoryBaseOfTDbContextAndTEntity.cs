using System;
using Abp.Data;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Transactions;

namespace Abp.Dapper.Repositories
{
    public class DapperEfRepositoryBase<TDbContext, TEntity> : DapperEfRepositoryBase<TDbContext, TEntity, Guid>, IDapperRepository<TEntity>
        where TEntity : class, IEntity<Guid>
        where TDbContext : class

    {
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }
    }
}
