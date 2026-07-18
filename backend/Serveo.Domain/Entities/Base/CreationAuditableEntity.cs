namespace Serveo.Domain.Entities.Base
{

    public abstract class CreationAuditableEntity : CreationAuditableEntity<Guid>
    {

    }

    public abstract class CreationAuditableEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreatedAt
    {
        public DateTimeOffset CreatedAt { get; set; }
    }
}
