using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    public class CouponUsage : Entity
    {
        public Guid CouponId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }

        public DateTimeOffset UsedAt { get; set; }
    }
}
