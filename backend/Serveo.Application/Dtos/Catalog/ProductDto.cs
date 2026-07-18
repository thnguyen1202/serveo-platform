namespace Serveo.Application.Dtos.Catalog
{
    public sealed class ProductDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public CategoryDto? Category { get; set; } = null;
        public ICollection<ProductTranslationDto>? Translations { get; set; } = null;
    }
}
