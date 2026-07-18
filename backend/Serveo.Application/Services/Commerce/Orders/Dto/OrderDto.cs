using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Services.Commerce.Orders.Dto
{
    public class OrderDto
    {
        public long Id { get; set; }

        public int DinerId { get; set; }
        public long? TableId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public long? DiscountId { get; set; }
        public string? DiscountCode { get; set; }
        public DiscountType? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        //public OrderStatusType Status { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new();

        public TableDto? Table { get; set; }
        public DinerDto? Diner { get; set; }
        public DiscountDto? Discount { get; set; }

        // common audit snapshot properties (optional)
        public DateTime CreationTime { get; set; }
        public long? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierId { get; set; }
    }

    public class OrderItemDto
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class TableDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
    }

    public class DinerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class DiscountDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public DiscountType? Type { get; set; }
        public decimal Value { get; set; }
    }
}