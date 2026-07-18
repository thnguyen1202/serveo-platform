using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Ordering
{
    // Một bàn có thể tạo nhiều order.
    public sealed class Order : CreationAuditableEntity
    {
        public Guid BranchId { get; set; }
        public Guid TableSessionId { get; set; }
        public Guid? DiscountId { get; set; }


        public long OrderNumber { get; set; }
        public OrderType OrderType { get; set; }


        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }


        public DiscountType? DiscountType { get; set; }
        public string? DiscountCode { get; set; }   // snapshot
        public decimal DiscountValue { get; set; }


        public OrderSource Source { get; set; }
        public OrderStatus Status { get; set; }


        public string? Notes { get; set; }


        public ICollection<OrderItem> Items { get; } = [];
        public TableSession TableSession { get; set; } = default!;
        public Branch Branch { get; set; } = default!;
        public Discount? Discount { get; set; }
    }

    public enum OrderSource : byte
    {
        QR = 1,
        Staff = 2,
        POS = 3,
    }

    public enum OrderStatus : byte
    {
        Pending = 1,
        Preparing = 2,
        Served = 3,
        Completed = 4,
        Cancelled = 5
    }

    public enum OrderType : byte
    {
        DineIn,
        TakeAway,
        Delivery
    }

}
