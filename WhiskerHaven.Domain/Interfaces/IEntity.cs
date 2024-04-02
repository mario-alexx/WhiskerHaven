namespace WhiskerHaven.Domain.Interfaces
{
    /// <summary>
    /// Interface representing an entity with a unique identifier.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        int Id { get; }
    }
}
