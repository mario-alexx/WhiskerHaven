using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;
using WhiskerHaven.Infrastructure.Repositories;

namespace WhiskerHaven.Test.Infrastructure.Repositories
{
    public class GenericRepositoryTests
    {
        [Fact]
        public async Task When_GetProductByIdAsync_ExistingEntity_Expect_ReturnProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var sut = new Product()
            {
                Id = 1,
                Name = "Excellent",
                Description = "Gato 10Kg",
                Stock = 2,
                Price = 5000,
                CategoryId = 1,
                UrlImage = "excellent.png"
            }; 

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Set<Product>().Add(sut);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new GenericRepository<Product>(context);

                // Act
                var result = await repository.GetByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(sut.Id, result.Id);
            }
        }

        [Fact]
        public async Task When_GetAllProductsAsync_Expect_ReturnsAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var sut = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Excellent",
                    Description = "Gato adulto 10Kg",
                    Stock = 2,
                    Price = 5000,
                    CategoryId = 1,
                    UrlImage = "excellent.png"
                },

                new Product
                {
                    Id = 2,
                    Name = "Vital Can",
                    Description = "Perro adulto 20Kg",
                    Stock = 5,
                    Price = 8000,
                    CategoryId = 1,
                    UrlImage = "vitalcan.png"
                }
            };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Set<Product>().AddRange(sut);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new GenericRepository<Product>(context);

                // Act
                var result = await repository.GetAllAsync();

                // Assert
                Assert.Equal(sut.Count, result.Count());
                foreach (var entity in sut)
                {
                    Assert.Contains(result, e => e.Id == entity.Id);
                }
            }
        }

        [Fact]
        public void When_AddProduct_Expect_AddProductContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var sut = new Product()
            {
                Id = 1,
                Name = "Excellent",
                Description = "Gato 10Kg",
                Stock = 2,
                Price = 5000,
                CategoryId = 1,
                UrlImage = "excellent.png"
            };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new GenericRepository<Product>(context);
                
                // Act
                repository.Add(sut);

                // Assert
                Assert.Contains(sut, context.Set<Product>().Local);
            }
        }

        [Fact]
        public void When_Update_Expect_UpdateProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var sut = new Product()
            {
                Id = 1,
                Name = "Excellent",
                Description = "Gato 10Kg",
                Stock = 2,
                Price = 5000,
                CategoryId = 1,
                UrlImage = "excellent.png"
            };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Set<Product>().Add(sut);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new GenericRepository<Product>(context);

                // Act
                sut.Description = "Gato 15Kg";
                repository.Update(sut);

                // Assert
                Assert.Equal("Gato 15Kg", context.Set<Product>().Find(sut.Id).Description);
            }
        }

        [Fact]
        public void When_Delete_Expect_RemovesProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var sut = new Product()
            {
                Id = 1,
                Name = "Excellent",
                Description = "Gato 10Kg",
                Stock = 2,
                Price = 5000,
                CategoryId = 1,
                UrlImage = "excellent.png"
            };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Set<Product>().Add(sut);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new GenericRepository<Product>(context);

                // Act
                repository.Delete(sut);

                // Assert
                Assert.DoesNotContain(sut, context.Set<Product>().Local);
            }
        }
    }
}
