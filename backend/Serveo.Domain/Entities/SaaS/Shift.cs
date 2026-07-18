using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    // Ca làm việc.
    public class Shift : Entity
    {
        public Guid StoreId { get; set; }


        [MaxLength(128)]
        public string Name { get; set; } = default!;


        public DateTimeOffset OpenedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }

        public Guid OpenedBy { get; set; }
        public Guid? ClosedBy { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OpeningCash { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ClosingCash { get; set; }
    }
}
