namespace Serveo.Application.Dtos.Catalog
{
    public sealed class ProductTranslationDto
    {
        public string LanguageCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
