using Serveo.Domain.Entities.Ordering;

namespace Serveo.Application.Services.Commerce.Orders.Dto
{
    public class CreateOrderDto
    {
        public Guid? DinerId { get; set; }

        public Guid? TableId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; } = new();

        public long? DiscountId { get; set; }

        public string? DiscountCode { get; set; }

        public DiscountType? DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal FeeAmount { get; set; }

        public string? Remarks { get; set; }
    }

    public class CreateOrderItemDto
    {
        public long MenuItemId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal UnitPrice { get; set; }

        public string? Notes { get; set; }
    }
}