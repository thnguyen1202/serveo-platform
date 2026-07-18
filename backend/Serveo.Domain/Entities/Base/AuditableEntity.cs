namespace Serveo.Domain.Entities.Base
{
    public abstract class AuditableEntity : AuditableEntity<Guid>
    {
    }

    public abstract class AuditableEntity<TPrimaryKey> : CreationAuditableEntity<TPrimaryKey>, IHasModifiedAt
    {
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}
