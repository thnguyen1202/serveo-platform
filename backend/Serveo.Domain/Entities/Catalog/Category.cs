using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class Category : FullAuditableEntity, IPassivable
    {
        public Guid BusinessId { get; set; }

        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;


        public Business Business { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = default!;
        public ICollection<CategoryTranslation> Translations { get; set; } = default!;
    }
}
