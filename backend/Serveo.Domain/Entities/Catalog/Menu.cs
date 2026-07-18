using Serveo.Domain.Entities.Base;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class Menu : FullAuditableEntity, IPassivable
    {
        public Guid BusinessId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;


        public Business Business { get; set; } = default!;
        public ICollection<MenuItem> Items { get; set; } = default!;
        public ICollection<MenuTranslation> Translations { get; set; } = default!;
    }
}
