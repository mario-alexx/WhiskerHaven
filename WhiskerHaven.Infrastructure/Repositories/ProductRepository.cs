using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;

namespace WhiskerHaven.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a repository implementation for products, inheriting from the generic repository for entities of type Product.
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Constructor for the ProductRepository class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves all products along with their associated categories.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of products.</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        /// <summary>
        /// Updates a product in the repository, including its image URL.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public void UpdateProduct(Product product)
        {
            // Retrieves the existing product with the same ID, including its image URL.
            var productImage = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
           
            if(productImage == null)
            {
                product.UrlImage = productImage.UrlImage;
            }
            _context.Update(product);
        }
    }
}
