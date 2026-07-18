using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Lịch sử điểm.
    public class LoyaltyTransaction : Entity
    {
        public Guid LoyaltyAccountId { get; set; }
        public LoyaltyTransactionType Type { get; set; }
        public int Points { get; set; }

        [MaxLength(64)]
        public string ReferenceType { get; set; } = default!;
        public Guid ReferenceId { get; set; }
    }

    public enum LoyaltyTransactionType : byte
    {
        Earn,
        Redeem,
        Adjust
    }
}
