namespace Serveo.WebApi.Models.Orders
{
    public class OrderModel
    {
        public string? Id { get; set; }
        public string? TableId { get; set; }
        public string? TableName { get; set; }
        public string Code { get; set; } = default!;
        public decimal SubTotal { get; set; }
        //public decimal DiscountAmount { get; set; }
        //public decimal FeeAmount { get; set; }
        //public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        //public OrderStatusType Status { get; set; }


        public ICollection<OrderItemModel> OrderItems { get; } = [];
    }

    public class OrderItemModel
    {
        public string? Id { get; set; }
        public string? MenuItemId { get; set; }
        public string? ItemName { get; set; }
        public long Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public decimal DiscountAmount { get; set; }
        //public decimal FeeAmount { get; set; }
        public decimal FinalPrice { get; set; }
        //public OrderItemStatusType Status { get; set; }
        public string? ImageUrl { get; set; }
    }
}
