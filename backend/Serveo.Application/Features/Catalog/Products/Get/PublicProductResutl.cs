using Serveo.Application.Dtos.Catalog;

namespace Serveo.Application.Features.Catalog.Products.Get
{
    public class PublicProductResutl
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string OptionGroup { get; set; } = string.Empty;
        public List<ProductDto> Variant { get; set; } = [];
        public List<ProductDto> Topping { get; set; } = [];
    }
}