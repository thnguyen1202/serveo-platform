namespace Serveo.Domain.Entities.Catalog
{
    public sealed class MenuCategory
    {
        public Guid MenuId { get; set; }
        public Guid CategoryId { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; } = true;


        public Menu Menu { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}
