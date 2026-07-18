namespace Serveo.Domain.Entities.Base
{
    /// <summary>
    /// An entity can implement this interface if <see cref="DeletedAt"/> of this entity must be stored.
    /// <see cref="DeletedAt"/> is automatically set when deleting <see cref="Entity"/>.
    /// </summary>
    public interface IHasDeletedAt : ISoftDelete
    {
        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        DateTimeOffset? DeletedAt { get; set; }
    }
}
