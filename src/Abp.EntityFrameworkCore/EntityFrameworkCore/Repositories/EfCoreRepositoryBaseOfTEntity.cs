using System;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Abp.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity<Guid>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}