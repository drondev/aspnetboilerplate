using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Represents a summary user
    /// </summary>
    [Table("AbpUserAccounts")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class UserAccount : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// Maximum length of the <see cref="UserName"/> property.
        /// </summary>
        public const int MaxUserNameLength = 256;

        /// <summary>
        /// Maximum length of the <see cref="EmailAddress"/> property.
        /// </summary>
        public const int MaxEmailAddressLength = 256;

        public virtual Guid? TenantId { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual Guid? UserLinkId { get; set; }

        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }
        
        //@todo users change this....
        public virtual Guid DronIamIdentity { get; set; }
    }
}