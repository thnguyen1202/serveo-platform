namespace Serveo.Domain.Entities.Base
{
    /// <summary>
    /// An entity can implement this interface if <see cref="ModifiedAt"/> of this entity must be stored.
    /// <see cref="ModifiedAt"/> is automatically set when updating <see cref="Entity"/>.
    /// </summary>
    public interface IHasModifiedAt
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTimeOffset? ModifiedAt { get; set; }
    }
}
