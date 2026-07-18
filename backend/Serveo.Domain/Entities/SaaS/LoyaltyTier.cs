using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    // Hạng thành viên. ex: Silver, Gold, Diamond
    public class LoyaltyTier : Entity
    {
        public Guid StoreId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = default!;
        public int MinPoints { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal DiscountPercent { get; set; }

    }
}
