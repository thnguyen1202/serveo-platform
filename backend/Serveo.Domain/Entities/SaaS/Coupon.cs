using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    public class Coupon : FullAuditableEntity
    {
        public Guid StoreId { get; set; }

        [MaxLength(64)]
        public string Code { get; set; } = default!;

        [MaxLength(256)]
        public string Name { get; set; } = default!;
        public CouponType Type { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int UsageLimit { get; set; }
        public byte Status { get; set; }
    }

    public enum CouponType : byte
    {
        Fixed,
        Percentage
    }
}
