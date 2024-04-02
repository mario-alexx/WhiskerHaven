using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Domain.Interfaces
{
    /// <summary>
    /// Interface representing a unit of work for managing transactions and repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Asynchronously saves changes made in the unit of work to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation that returns the number of state entries written to the data store.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Add repositories to work with the UnitOfWork

        /// <summary>
        /// Gets the repository for managing products.
        /// </summary>
        IGenericRepository<Product> ProductRepository { get; }

        /// <summary>
        /// Gets the repository for managing categories.
        /// </summary>
        IGenericRepository<Category> CategoryRepository { get; }

        /// <summary>
        /// Gets the repository for managing products with additional methods.
        /// </summary>
        IProductRepository ProductsRepository { get; }

        /// <summary>
        /// Gets the repository for managing users.
        /// </summary>
        IUserRepository UserRepository { get; }
    }
}
