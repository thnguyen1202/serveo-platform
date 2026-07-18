using System.Text.Json.Serialization;

namespace Serveo.Application.Dtos
{
    public class EntityDto : EntityDto<Guid>, IEntityDto { }

    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        [JsonPropertyOrder(-1)]
        public virtual TPrimaryKey Id { get; set; } = default!;
    }
}
