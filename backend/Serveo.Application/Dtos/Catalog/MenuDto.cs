namespace Serveo.Application.Dtos.Catalog
{
    public sealed class MenuDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }


        public ICollection<ProductDto>? Products { get; set; } = null;
        public ICollection<MenuTranslationDto>? Translations { get; set; } = null;
    }
}
