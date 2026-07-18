using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class CategoryTranslation : ITranslation
    {
        public Guid CategoryId { get; set; }
        public string LanguageCode { get; set; } = default!;
        public string Name { get; set; } = default!;

        public Category Category { get; set; } = default!;
    }
}
