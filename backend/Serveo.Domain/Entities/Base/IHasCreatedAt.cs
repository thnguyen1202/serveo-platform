namespace Serveo.Domain.Entities.Base
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreatedAt"/> of this entity must be stored.
    /// <see cref="CreatedAt"/> is automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface IHasCreatedAt
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }
    }
}
