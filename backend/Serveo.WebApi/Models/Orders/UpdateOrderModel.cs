namespace Serveo.WebApi.Models.Orders
{
    public class UpdateOrderModel : CreateOrderModel, IEntityModel<long>
    {
        public long Id { get; set; }
    }
}
