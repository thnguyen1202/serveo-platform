using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Ordering
{
    public sealed class OrderSession : CreationAuditableEntity
    {
        public long SessionNumber { get; set; }
        public string SessionCode { get; set; } = default!;

        public DateTimeOffset OpenedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }

        public int GuestCount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public string? Notes { get; set; }

        public OrderSessionStatus Status { get; set; }
    }

    public enum OrderSessionStatus : byte
    {
        Open = 1,
        Closed = 2,
        Cancelled = 3
    }
}
