using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.SaaS
{
    // Mở rộng giao hàng sau này.
    public class CustomerAddress : Entity
    {
        public Guid CustomerId { get; set; }

        [MaxLength(128)]
        public string? Label { get; set; }

        [MaxLength(512)]
        public string? AddressLine { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Longitude { get; set; }

        public bool IsDefault { get; set; }
    }
}
