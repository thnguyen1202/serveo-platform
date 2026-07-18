using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Ordering;
using Serveo.Domain.Entities.Tenanting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS.Payment
{
    // Một order có thể nhiều payment.
    public class Payment : Entity
    {
        public Guid BusinessId { get; set; }
        public Guid TableSessionId { get; set; }

        public long PaymentNumber { get; set; }
        public PaymentMethod Method { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Amount { get; set; }

        [MaxLength(256)]
        public string? TransactionId { get; set; }

        public decimal Status { get; set; }

        public DateTimeOffset PaidAt { get; set; }


        [ForeignKey(nameof(BusinessId))]
        public Business Business { get; set; } = default!;

        [ForeignKey(nameof(TableSessionId))]
        public TableSession TableSession { get; set; } = default!;
    }

    public enum PaymentMethod : byte
    {
        Cash = 1,
        BankTransfer,
        QR,
        Momo,
        ZaloPay,
        Card,
        EWallet,
        Other
    }

    public enum PaymentStatus : byte
    {
        Pending = 1,
        Paid = 2,
        Failed = 3,
        Refunded = 4,
        Cancelled = 5
    }
}
