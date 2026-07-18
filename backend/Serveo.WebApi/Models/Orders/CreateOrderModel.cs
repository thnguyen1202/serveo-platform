namespace Serveo.WebApi.Models.Orders
{
    public class CreateOrderModel
    {
        //public int RestaurantId { get; set; }
        public string BookingId { get; set; } = default!;
        //public decimal TotalAmout { get; set; }

        public List<CreateUpdateOrderItemModel> OrderItems { get; set; } = default!;
    }
}
