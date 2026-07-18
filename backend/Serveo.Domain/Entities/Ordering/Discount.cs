using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Ordering
{
    public class Discount : CreationAuditableEntity, IPassivable
    {
        public Guid BranchId { get; set; }

        public string Code { get; set; } = default!;
        public DiscountType Type { get; set; }     // Percent / Fixed
        public decimal Value { get; set; }
        public decimal? MaxDiscountAmount { get; set; }

        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }

        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }

        public bool IsActive { get; set; }


        public Branch Branch { get; set; } = default!;
    }

    public enum DiscountType : byte
    {
        FixedAmount = 1,
        Percentage = 2
    }
}
