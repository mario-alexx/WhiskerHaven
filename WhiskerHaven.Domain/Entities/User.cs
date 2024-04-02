using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Domain.Entities
{
    /// <summary>
    /// Represents a user entity with common properties inherited from BaseEntity and IEntity.
    /// </summary>
    public class User : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string? PhoneNumber { get; set; }

        // Llave foranea con la entidad Role
        //public int RoleId { get; set; }
        //public virtual Role Role { get; set; } = new Role();

    }
}
