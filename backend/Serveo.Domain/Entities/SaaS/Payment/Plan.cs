using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS.Payment
{
    public class Plan : Entity
    {
        public string Name { get; set; } = default!;

        [Column(TypeName = DbDecimals.Money)]
        public decimal MonthlyFee { get; set; }

        [Column(TypeName = DbDecimals.Money)]
        public decimal FeePerOrder { get; set; }

        public string? FeaturesJson { get; set; }
    }
}
