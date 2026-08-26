namespace Serveo.Domain.Entities.Tenanting
{
    public class TenantMemberBranch
    {
        public Guid TenantMemberId { get; private set; }
        public Guid BranchId { get; private set; }


        public TenantMember TenantMember { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
    }
}
