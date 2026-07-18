using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    // CRM khách hàng.
    public class Customer : FullAuditableEntity
    {
        public Guid Stored { get; set; }

        [MaxLength(256)]
        public string Name { get; set; } = default!;

        [MaxLength(36)]
        public string? Phone { get; set; }

        [MaxLength(256)]
        public string? Email { get; set; }

        public DateOnly Birthday { get; set; }
        public byte Gender { get; set; }
        public byte Status { get; set; }
    }
}
