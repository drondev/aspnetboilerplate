using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.DynamicEntityProperties
{
    [Table("AbpDynamicEntityProperties")]
    public class DynamicEntityProperty : Entity, IMayHaveTenant
    {
        public string EntityFullName { get; set; }

        [Required]
        public Guid DynamicPropertyId { get; set; }

        public Guid? TenantId { get; set; }
        
        [ForeignKey("DynamicPropertyId")]
        public virtual DynamicProperty DynamicProperty { get; set; }

    }
}
