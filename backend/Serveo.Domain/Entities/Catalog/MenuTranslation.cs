using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Catalog
{
    public sealed class MenuTranslation : ITranslation
    {
        public Guid MenuId { get; set; }
        public string LanguageCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public Menu Menu { get; set; } = default!;
    }
}
