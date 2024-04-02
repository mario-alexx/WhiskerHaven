using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Models.Product
{
    public record class ProductResponseModel(int Id, string Name, string Description, int Stock, decimal Price, CategoryResponseModel Category ,string UrlImage);

    public record class AddProductResponseModel(int Id, string Name, string Description, int Stock, decimal Price, int CategoryId, string UrlImage);
}
