using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class Translation : Entity
    {
        public Guid LanguageId { get; set; }

        [MaxLength(64)]
        public string EntityName { get; set; } = default!;

        [MaxLength(64)]
        public string PropertyName { get; set; } = default!;


        [MaxLength(128)]
        public string EntityId { get; set; } = default!;

        public string? Value { get; set; }

        #region Relationships
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; } = default!;
        #endregion
    }
}
