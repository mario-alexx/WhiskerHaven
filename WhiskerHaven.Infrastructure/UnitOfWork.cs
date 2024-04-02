using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;

namespace WhiskerHaven.Infrastructure
{
    /// <summary>
    /// Represents a unit of work for managing transactions and repositories.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Gets the repository for managing categories.
        /// </summary>
        public IGenericRepository<Category> CategoryRepository { get; }

        /// <summary>
        /// Gets the repository for managing products.
        /// </summary>
        public IGenericRepository<Product> ProductRepository { get; }

        /// <summary>
        /// Gets the repository for managing products with additional methods.
        /// </summary>
        public IProductRepository ProductsRepository { get; }

        /// <summary>
        /// Gets the repository for managing users.
        /// </summary>
        public IUserRepository UserRepository { get; }

        /// <summary>
        /// Constructor for the UnitOfWork class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="categoryRepository">The repository for managing categories.</param>
        /// <param name="productRepository">The repository for managing products.</param>
        /// <param name="userRepository">The repository for managing users.</param>
        /// <param name="productsRepository">The repository for managing products with additional methods.</param>
        public UnitOfWork(ApplicationDbContext context, IGenericRepository<Category> categoryRepository, IGenericRepository<Product> productRepository, IUserRepository userRepository, IProductRepository productsRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            UserRepository = userRepository;
            ProductsRepository = productsRepository;
        }

        /// <summary>
        /// Asynchronously saves changes made in the unit of work to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation that returns the number of state entries written to the data store.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes the database context.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
