using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Models.Product
{
    public record class ProductRequestModel(string Name, string Description, int Stock, decimal Price, string UrlImage, int CategoryId);
}
