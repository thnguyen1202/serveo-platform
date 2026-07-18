using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS.Payment
{
    // trả theo tháng
    public class Subscription : Entity
    {
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public byte Status { get; set; }

    }
}
