using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Domain.Interfaces
{
    /// <summary>
    /// Interface representing a repository for products, inheriting from the generic repository for entities of type Product.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// Asynchronously retrieves all products.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of products.</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
