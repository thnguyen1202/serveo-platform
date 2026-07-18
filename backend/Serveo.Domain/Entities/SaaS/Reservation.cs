using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    public class Reservation : Entity, IHasCreatedAt
    {
        public Guid StoreId { get; set; }

        [MaxLength(256)]
        public string CustomerName { get; set; } = default!;

        [MaxLength(36)]
        public string Phone { get; set; } = default!;

        public int GuestCount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public byte Status { get; set; }
    }
}
