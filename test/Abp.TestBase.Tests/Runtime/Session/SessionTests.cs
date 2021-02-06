using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using Abp.Tests;
using Shouldly;
using Xunit;

namespace Abp.TestBase.Tests.Runtime.Session
{
    public class SessionTests : AbpIntegratedTestBase<AbpKernelModule>
    {
        [Fact]
        public void Should_Be_Default_On_Startup()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(1.ToGuid());

            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(null);
        }

        [Fact]
        public void Can_Change_Session_Variables()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId = 1.ToGuid();
            AbpSession.TenantId = 42.ToGuid();

            var resolvedAbpSession = LocalIocManager.Resolve<IAbpSession>();

            resolvedAbpSession.UserId.ShouldBe(1.ToGuid());
            resolvedAbpSession.TenantId.ShouldBe(42.ToGuid());

            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(1.ToGuid());
            AbpSession.TenantId.ShouldBe(1.ToGuid());
        }
    }
}
