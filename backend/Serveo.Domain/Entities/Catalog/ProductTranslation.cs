using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class ProductTranslation : ITranslation
    {
        public Guid ProductId { get; set; }
        public string LanguageCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public Product Product { get; set; } = default!;
    }
}
