namespace Serveo.Application.Dtos.Catalog
{
    public sealed class MenuItemDto
    {
        public Guid MenuId { get; set; }
        public Guid ProductId { get; set; }


        public MenuDto Menu { get; set; } = default!;
        public ProductDto Product { get; set; } = default!;
    }
}
