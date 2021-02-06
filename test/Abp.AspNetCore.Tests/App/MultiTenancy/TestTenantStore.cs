using System.Collections.Generic;
using System.Linq;
using Abp.MultiTenancy;

namespace Abp.AspNetCore.App.MultiTenancy
{
    public class TestTenantStore : ITenantStore
    {
        private readonly List<TenantInfo> _tenants = new List<TenantInfo>
        {
            new TenantInfo(1.ToGuid(), "Default"),
            new TenantInfo(42.ToGuid(), "acme"),
            new TenantInfo(43.ToGuid(), "vlsft")
        };

        public TenantInfo Find(Guid tenantId)
        {
            return _tenants.FirstOrDefault(t => t.Id == tenantId);
        }

        public TenantInfo Find(string tenancyName)
        {
            return _tenants.FirstOrDefault(t => t.TenancyName.ToLower() == tenancyName.ToLower());
        }
    }
}
