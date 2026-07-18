using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Tenanting
{
    public sealed class Tenant : FullAuditableEntity
    {
        public Guid SubscriptionPlanId { get; set; }

        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;

        //[MaxLength(64)]
        //public string? TenancyName { get; set; }

        //public byte PlanType { get; set; }

        public TenantBillingType BillingType { get; set; }

        public DateTimeOffset ExpiredTime { get; set; }

        //public bool IsActive { get; set; } = true;

        public byte Status { get; set; }
    }

    public enum TenantBillingType : byte
    {
        Subscription = 1,
        Hybrid,
        PerOrder
    }

}
