using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // Đánh giá khách hàng.
    public class CustomerFeedback : Entity
    {
        public Guid StoreId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public int Rating { get; set; }

        [MaxLength(1024)]
        public string? Comment { get; set; }

    }
}
