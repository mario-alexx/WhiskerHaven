using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Domain.Entities
{
    /// <summary>
    /// Representa el rol que un User puede tener
    /// </summary>
    public class Role : BaseEntity, IEntity
    {
        /// <summary>
        /// Llave primaria de la entidad Role
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Campo de referencia con User
        /// </summary>
        //public ICollection<User> Users { get; set; }
    }
}
