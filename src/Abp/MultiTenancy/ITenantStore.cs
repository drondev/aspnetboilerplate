using System;
using JetBrains.Annotations;

namespace Abp.MultiTenancy
{
    public interface ITenantStore
    {
        [CanBeNull]
        TenantInfo Find(Guid tenantId);

        [CanBeNull]
        TenantInfo Find([NotNull] string tenancyName);
    }
}