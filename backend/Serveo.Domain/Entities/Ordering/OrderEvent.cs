using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.Ordering
{
    // Audit log cực quan trọng.
    public sealed class OrderEvent : Entity
    {
        public Guid OrderId { get; set; }

        [MaxLength(64)]
        public string EventType { get; set; } = default!;

        public string? EventData { get; set; }
    }
}
