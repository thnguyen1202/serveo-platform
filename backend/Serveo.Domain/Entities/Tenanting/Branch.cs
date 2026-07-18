using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Tenanting
{
    public sealed class Branch : FullAuditableEntity, IPassivable
    {
        public Guid BusinessId { get; set; }

        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public bool IsActive { get; set; }


        public Business Business { get; set; } = default!;
    }
}
