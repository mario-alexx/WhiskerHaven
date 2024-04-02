using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Domain.Entities
{
    /// <summary>
    /// Represents a product entity with common properties inherited from BaseEntity and IEntity.
    /// </summary>
    public class Product : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category to which the product belongs.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category to which the product belongs.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product image.
        /// </summary>
        public string? UrlImage { get; set; }
    }
}
