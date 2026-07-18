using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.SaaS
{
    public class OutboxMessage : CreationAuditableEntity
    {
        public string Type { get; set; } = default!;
        public string Payload { get; set; } = default!;
        public bool IsProcessed { get; set; }
    }
}
