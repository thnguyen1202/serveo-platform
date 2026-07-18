using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS.Kitchen
{
    public class KitchenTicketItem : Entity
    {
        public Guid KitchenTicketId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }

        [MaxLength(256)]
        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        [MaxLength(512)]
        public string? Notes { get; set; }

        public bool Status { get; set; }
    }

    public enum KitchenTicketItemStatus : byte
    {
        Pending = 1,
        Preparing = 2,
        Ready = 3,
        Completed = 4,
        Cancelled = 5
    }
}
