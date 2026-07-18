namespace Serveo.Application.Services.Commerce.Orders.Dto
{
    public class UpdateOrderDto : CreateOrderDto
    {
        public Guid? Id { get; set; }
    }
}