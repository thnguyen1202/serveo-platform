using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class Product : FullAuditableEntity
    {
        public Guid BusinessId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? StationId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }


        public Business Business { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public ICollection<MenuItem> MenuItems { get; set; } = default!;
        public ICollection<ProductTranslation> Translations { get; set; } = default!;
    }
}
