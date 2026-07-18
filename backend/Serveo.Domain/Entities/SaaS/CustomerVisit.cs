using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    // Theo dõi tần suất quay lại.
    public class CustomerVisit : Entity
    {
        public Guid CustomerId { get; set; }
        public Guid TableSessionId { get; set; }
        public DateTimeOffset VisitDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Guid SpendAmount { get; set; }
    }
}
