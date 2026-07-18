using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS.Payment
{
    // Lưu log gateway.
    public class PaymentTransaction : Entity, IHasCreatedAt
    {
        public Guid PaymentId { get; set; }


        [MaxLength(128)]
        public string Provider { get; set; } = default!;

        [MaxLength(256)]
        public string ProviderTransactionId { get; set; } = default!;


        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }


        public DateTimeOffset CreatedAt { get; set; }


        public PaymentTransaction()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
