namespace Serveo.Application.Dtos.Catalog
{
    public sealed class CategoryDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public ICollection<ProductDto> Products { get; set; } = [];
        public ICollection<CategoryTranslationDto> Translations { get; set; } = [];
    }
}
