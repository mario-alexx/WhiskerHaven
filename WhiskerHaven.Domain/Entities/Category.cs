using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Domain.Entities
{
    /// <summary>
    /// Represents a category entity with common properties inherited from BaseEntity and IEntity.
    /// </summary>
    public class Category : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category. This property is required.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of products associated with this category.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}

