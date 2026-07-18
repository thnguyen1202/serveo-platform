using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS.Kitchen
{
    // Bếp nhận phiếu chế biến.
    public class KitchenTicket : AuditableEntity
    {
        public Guid StoreId { get; set; }
        public Guid TableSessionId { get; set; }
        public Guid OrderId { get; set; }
        public Guid StationId { get; set; }

        public long TicketNumber { get; set; }

        public KitchenTicketStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public enum KitchenTicketStatus : byte
    {
        Pending = 1,
        Accepted = 2,
        Preparing = 3,
        Ready = 4,
        Completed = 5,
        Cancelled = 6
    }
}
