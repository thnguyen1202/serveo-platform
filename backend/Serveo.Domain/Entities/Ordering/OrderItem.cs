using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Catalog;

namespace Serveo.Domain.Entities.Ordering
{
    public sealed class OrderItem : Entity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? DiscountId { get; set; }


        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }

        public decimal DiscountAmount { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal FinalPrice { get; set; }

        public string? DiscountCode { get; set; }   // snapshot
        public DiscountType? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        public string Notes { get; set; } = default!;
        public byte Status { get; set; }


        public Order Order { get; set; } = default!;
        public Product? Product { get; set; }
        public Discount? Discount { get; set; }
    }

    public enum OrderItemStatus : byte
    {
        Pending = 1,
        Preparing = 2,
        Ready = 3,
        Served = 4,
        Completed = 5,
        Cancelled = 6
    }
}
