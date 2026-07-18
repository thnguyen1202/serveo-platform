using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.Domain.Entities.SaaS
{
    public class Station : Entity
    {
        public Guid StoreId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = default!;

        [MaxLength(64)]
        public string Code { get; set; } = default!;

        [MaxLength(512)]
        public string? Description { get; set; }
    }
}
