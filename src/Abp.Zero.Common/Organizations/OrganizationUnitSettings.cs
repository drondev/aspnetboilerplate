using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Zero.Configuration;

namespace Abp.Organizations
{
    /// <summary>
    /// Implements <see cref="IOrganizationUnitSettings"/> to get settings from <see cref="ISettingManager"/>.
    /// </summary>
    public class OrganizationUnitSettings : IOrganizationUnitSettings, ITransientDependency
    {
        /// <summary>
        /// Maximum allowed organization unit membership count for a user.
        /// Returns value for current tenant.
        /// </summary>
        public int MaxUserMembershipCount
        {
            get
            {
                return _settingManager.GetSettingValue<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount);
            }
        }

        private readonly ISettingManager _settingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationUnitSettings"/> class.
        /// </summary>
        public OrganizationUnitSettings(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// Maximum allowed organization unit membership count for a user.
        /// Returns value for given tenant.
        /// </summary>
        public async Task<int> GetMaxUserMembershipCountAsync(Guid? tenantId)
        {
            if (tenantId.HasValue)
            {
                return await _settingManager.GetSettingValueForTenantAsync<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, tenantId.Value);
            }
            else
            {
                return await _settingManager.GetSettingValueForApplicationAsync<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount);
            }
        }

        /// <summary>
        /// Maximum allowed organization unit membership count for a user.
        /// Returns value for given tenant.
        /// </summary>
        public int GetMaxUserMembershipCount(Guid? tenantId)
        {
            if (tenantId.HasValue)
            {
                return _settingManager.GetSettingValueForTenant<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, tenantId.Value);
            }
            else
            {
                return _settingManager.GetSettingValueForApplication<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount);
            }
        }

        public async Task SetMaxUserMembershipCountAsync(Guid? tenantId, int value)
        {
            if (tenantId.HasValue)
            {
                await _settingManager.ChangeSettingForTenantAsync(tenantId.Value, AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
            else
            {
                await _settingManager.ChangeSettingForApplicationAsync(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
        }

        public void SetMaxUserMembershipCount(Guid? tenantId, int value)
        {
            if (tenantId.HasValue)
            {
                _settingManager.ChangeSettingForTenant(tenantId.Value, AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
            else
            {
                _settingManager.ChangeSettingForApplication(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
        }
    }
}