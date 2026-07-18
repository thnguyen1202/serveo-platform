using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    // cho per-order billing
    public class Usage : CreationAuditableEntity
    {
        public Guid TenantId { get; set; }
        public Guid OrderId { get; set; }

        [Column(TypeName = DbDecimals.Money)]
        public decimal Amount { get; set; }

        [Column(TypeName = DbDecimals.Money)]
        public decimal FeeAmount { get; set; } // (phí SaaS)

    }
}
