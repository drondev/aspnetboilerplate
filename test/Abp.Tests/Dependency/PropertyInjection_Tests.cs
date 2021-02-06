using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Extensions;
using Abp.Runtime.Session;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Abp.Tests.Dependency
{
    public class PropertyInjection_Tests : TestBaseWithLocalIocManager
    {
        [Fact]
        public void Should_Inject_Session_For_ApplicationService()
        {
            var session = Substitute.For<IAbpSession>();
            session.TenantId.Returns(1.ToGuid());
            session.UserId.Returns(42.ToGuid());

            LocalIocManager.Register<MyApplicationService>();
            LocalIocManager.IocContainer.Register(
                Component.For<IAbpSession>().Instance(session)
                );

            var myAppService = LocalIocManager.Resolve<MyApplicationService>();
            myAppService.TestSession();
        }

        private class MyApplicationService : ApplicationService
        {
            public void TestSession()
            {
                AbpSession.ShouldNotBe(null);
                AbpSession.TenantId.ShouldBe(1.ToGuid());
                AbpSession.UserId.ShouldBe(42.ToGuid());
            }
        }
    }
}
