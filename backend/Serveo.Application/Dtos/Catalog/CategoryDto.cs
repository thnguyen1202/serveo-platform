namespace Serveo.Application.Dtos.Catalog
{
    public sealed class CategoryDto
    {
        public string Name { get; set; } = default!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<ProductDto>? Products { get; set; } = null;
        public ICollection<CategoryTranslationDto>? Translations { get; set; } = null;
    }
}
