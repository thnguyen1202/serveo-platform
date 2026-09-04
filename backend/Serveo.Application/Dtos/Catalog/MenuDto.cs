namespace Serveo.Application.Dtos.Catalog
{
    public sealed class MenuDto : EntityDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public bool IsActive { get; set; }


        public ICollection<CategoryDto> Categories { get; set; } = [];
        public ICollection<MenuTranslationDto> Translations { get; set; } = [];
    }
}
