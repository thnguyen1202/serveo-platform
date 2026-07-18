using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    public class CashTransaction : AuditableEntity
    {
        public Guid ShiftId { get; set; }

        public CashTransactionType Type { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Amount { get; set; }

        [MaxLength(512)]
        public string? Reason { get; set; }
    }

    public enum CashTransactionType : byte
    {
        CashIn = 1,
        CashOut = 2
    }
}
