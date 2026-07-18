using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    // Ví điểm thưởng.
    public class LoyaltyAccount : Entity
    {
        public Guid CustomerId { get; set; }

        public int CurrentPoints { get; set; }
        public int LifetimePoints { get; set; }
        public Guid? TierId { get; set; }
    }
}
