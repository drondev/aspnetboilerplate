using System;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.Zero.SampleApp.MultiTenancy;
using Abp.Zero.SampleApp.Users;

namespace Abp.Zero.SampleApp.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, User>
    {
        public FeatureValueStore(ICacheManager cacheManager,
            IRepository<TenantFeatureSetting, Guid> tenantFeatureRepository,
            IRepository<Tenant> tenantRepository,
            IRepository<EditionFeatureSetting, Guid> editionFeatureRepository,
            IFeatureManager featureManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                  cacheManager, 
                  tenantFeatureRepository, 
                  tenantRepository, 
                  editionFeatureRepository, 
                  featureManager,
                  unitOfWorkManager)
        {

        }
    }
}