using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class Permission : Entity
    {
        public string Code { get; private set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; private set; }
        public string Module { get; private set; } = null!;
    }
}
