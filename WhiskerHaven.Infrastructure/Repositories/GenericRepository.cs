using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;

namespace WhiskerHaven.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a generic repository implementation for entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entities managed by the repository.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// The database context.
        /// </summary>
        public ApplicationDbContext _context;

        /// <summary>
        /// Constructor for the GenericRepository class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task representing the asynchronous operation that returns the entity, or null if not found.</returns>

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion

        #region Commands
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
        }
        #endregion
    }
}
