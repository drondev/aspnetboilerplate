﻿using System;
using System.Threading.Tasks;

namespace Abp.DynamicEntityProperties
{
    public interface IDynamicPropertyPermissionChecker
    {
        void CheckPermission(Guid dynamicPropertyId);

        Task CheckPermissionAsync(Guid dynamicPropertyId);

        bool IsGranted(Guid dynamicPropertyId);

        Task<bool> IsGrantedAsync(Guid dynamicPropertyId);
    }
}
