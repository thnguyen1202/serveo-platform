using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Identity;

namespace Serveo.Domain.Entities.Tenanting
{
    public class TenantMember : Entity, IMustHaveTenant, IMustHaveUser
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; private set; }

        public bool IsAllBranches { get; private set; }
        public DateTimeOffset JoinedAt { get; private set; } = DateTimeOffset.UtcNow;


        public Tenant Tenant { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public Role Role { get; private set; } = null!;
        public ICollection<TenantMemberBranch> TenantMemberBranches { get; private set; } = [];

    }
}
