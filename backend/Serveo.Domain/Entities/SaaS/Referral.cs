using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    public class Referral : Entity
    {
        public Guid ProgramId { get; set; }
        public Guid ReferrerCustomerId { get; set; }
        public Guid ReferredCustomerId { get; set; }
        public bool RewardGranted { get; set; }
    }
}
