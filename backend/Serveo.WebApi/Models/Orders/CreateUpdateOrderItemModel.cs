namespace Serveo.WebApi.Models.Orders
{
    public class CreateUpdateOrderItemModel
    {
        public string ItemId { get; set; } = default!;
        public long Quantity { get; set; }
    }
}
