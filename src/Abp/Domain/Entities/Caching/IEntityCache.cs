using System;
using Abp.Runtime.Caching;

namespace Abp.Domain.Entities.Caching
{
    public interface IEntityCache<TCacheItem> : IEntityCache<TCacheItem, Guid>
    {
    }

    public interface IEntityCache<TCacheItem, TPrimaryKey> : IEntityCacheBase<TCacheItem, TPrimaryKey>
    {
        ITypedCache<TPrimaryKey, TCacheItem> InternalCache { get; }
    }
}