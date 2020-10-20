using System;
using Abp.Domain.Entities;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Used to store setting for a permission for a user.
    /// </summary>
    public class UserPermissionSetting : PermissionSetting, IEntity<Guid>
    {
        /// <summary>
        /// User id.
        /// </summary>
        public virtual Guid UserId { get; set; }
    }
}