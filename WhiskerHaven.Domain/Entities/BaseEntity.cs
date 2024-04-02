using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Domain.Entities
{
    /// <summary>
    /// Base entity class representing common properties for entities.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created in UTC format.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated in UTC format.
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating the status of the entity.
        /// </summary>
        public bool Status { get; set; } = true;
    }
}
