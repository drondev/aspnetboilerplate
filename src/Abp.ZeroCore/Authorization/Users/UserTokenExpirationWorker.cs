﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;

namespace Abp.Authorization.Users
{
    public class UserTokenExpirationWorker<TTenant, TUser> : PeriodicBackgroundWorkerBase
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUserBase
    {
        private readonly IRepository<UserToken, Guid> _userTokenRepository;
        private readonly IRepository<TTenant, Guid> _tenantRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserTokenExpirationWorker(
            AbpTimer timer,
            IRepository<UserToken, Guid> userTokenRepository,
            IBackgroundJobConfiguration backgroundJobConfiguration,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<TTenant, Guid> tenantRepository)
            : base(timer)
        {
            _userTokenRepository = userTokenRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _tenantRepository = tenantRepository;

            Timer.Period = backgroundJobConfiguration.UserTokenExpirationPeriod?.TotalMilliseconds.To<int>()
                           ?? backgroundJobConfiguration.CleanUserTokenPeriod
                           ?? TimeSpan.FromHours(1).TotalMilliseconds.To<int>();
        }

        protected override void DoWork()
        {
            List<Guid> tenantIds;
            var utcNow = Clock.Now.ToUniversalTime();

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    _userTokenRepository.Delete(t => t.ExpireDate <= utcNow);
                    tenantIds = _tenantRepository.GetAll().Select(t => t.Id).ToList();
                    uow.Complete();
                }
            }

            foreach (var tenantId in tenantIds)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        _userTokenRepository.Delete(t => t.ExpireDate <= utcNow);
                        uow.Complete();
                    }
                }
            }
        }
    }
}
