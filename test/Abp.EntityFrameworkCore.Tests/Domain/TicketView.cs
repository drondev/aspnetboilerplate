using Abp.Domain.Entities;
using Abp.Tests;

namespace Abp.EntityFrameworkCore.Tests.Domain
{
    public class TicketListItem : IPassivable, IMustHaveTenant, IEntity<Guid>
    {
        public int Id { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual GuidStatics TenantId { get; set; }

        public bool IsTransient()
        {
            return Id <= 0.ToGuid();
        }
    }
}
