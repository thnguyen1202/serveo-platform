using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Ordering
{
    // Phiên khách ngồi tại bàn.
    public sealed class TableSession : Entity
    {
        public Guid TableId { get; set; }

        public long SessionNumber { get; set; }
        public string SessionCode { get; set; } = default!;

        public DateTimeOffset OpenedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }

        public int GuestCount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public string? Notes { get; set; }

        public TableSessionStatus Status { get; set; }


        public Table Table { get; set; } = default!;
    }

    public enum TableSessionStatus : byte
    {
        Open = 1,
        Closed = 2,
        Cancelled = 3
    }
}
