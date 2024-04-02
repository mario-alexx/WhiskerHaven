using AutoMapper;
using WhiskerHaven.Application.Models.Product;
using WhiskerHaven.Application.Services.ProductService.Commands.CreateProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Profiles
{
    /// <summary>
    /// Represents a mapping profile for product-related operations.
    /// </summary>
    public class ProductProfile : Profile
    {
        /// <summary>
        /// Constructor for the ProductProfile class.
        /// </summary>
        public ProductProfile()
        {
            // Maps from CreateProductCommand to Product and vice versa.
            CreateMap<CreateProductCommand, Product>().ReverseMap();

            // Maps from UpdateProductCommand to Product and vice versa.
            CreateMap<UpdateProductCommand, Product>().ReverseMap();

            // Maps from Product to ProductResponseModel and vice versa.
            CreateMap<Product, ProductResponseModel>().ReverseMap();

            // Maps from Product to AddProductResponseModel and vice versa.
            CreateMap<Product, AddProductResponseModel>().ReverseMap();
        }
    }
}
