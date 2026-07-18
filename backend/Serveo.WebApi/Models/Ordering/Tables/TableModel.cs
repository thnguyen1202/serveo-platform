using Serveo.WebApi.Models.Orders;

namespace Serveo.WebApi.Models.Ordering.Tables
{
    public class TableModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        //public int RestaurantId { get; set; }
        public string QrToken { get; set; } = default!;
        public int Capacity { get; set; }
        //public bool IsActive { get; set; }
        //public TableStatusType Status { get; set; }
        public DateTime CreationTime { get; set; }

        public OrderModel? Order { get; set; }
    }
}
