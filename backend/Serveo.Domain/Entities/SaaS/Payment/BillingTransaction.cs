using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS.Payment
{
    // Hybrid SaaS revenue
    public class BillingTransaction : CreationAuditableEntity
    {
        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; } // (SubscriptionId / OrderId)

        public BillingTransactionType Type { get; set; }

        [Column(TypeName = DbDecimals.Money)]
        public decimal Amount { get; set; }
    }

    public enum BillingTransactionType : byte
    {
        Subscription,
        OrderFee
    }
}
