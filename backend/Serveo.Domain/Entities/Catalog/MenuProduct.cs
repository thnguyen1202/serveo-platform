using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class MenuProduct: Entity
    {
        public Guid MenuId { get; set; }
        public Guid ProductId { get; set; }

        public int DisplayOrder { get; set; }
        public decimal? PriceOverride { get; set; }
        public bool IsVisible { get; set; } = true;


        public Menu Menu { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
