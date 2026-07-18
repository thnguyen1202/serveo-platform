using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    public class CustomerMetrics : Entity
    {
        public Guid CustomerId { get; set; }
        public int VisitCount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalSpend { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AverageSpend { get; set; }
        public DateTimeOffset LastVisitAt { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LifetimeValue { get; set; }
    }
}
