namespace Serveo.Domain.Entities.Base
{
    public abstract class FullAuditableEntity : FullAuditableEntity<Guid>
    {
    }

    public abstract class FullAuditableEntity<TPrimaryKey> : AuditableEntity<TPrimaryKey>, IHasDeletedAt
    {
        public DateTimeOffset? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
