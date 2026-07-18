using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Entities.Tenanting;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    public class Staff : Entity, IHasCreatedAt, IPassivable, IMustHaveUser
    {
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }

        public StaffRoleType Role { get; set; } = StaffRoleType.Staff;

        public bool IsActive { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; }


        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = default!;

        [ForeignKey(nameof(StoreId))]
        public Business Store { get; set; } = default!;

    }

    public enum StaffRoleType : byte
    {
        Staff,
        Manager
    }
}
