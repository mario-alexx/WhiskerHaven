using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven
{
    /// <summary>
    /// Interface representing a generic repository for entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entities managed by the repository.</typeparam>
    public interface IGenericRepository<T> where T : IEntity
    {
        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an entity of type T by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task representing the asynchronous operation that returns the entity, or null if not found.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity of type T to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity of type T in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity of type T from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
    }
}
