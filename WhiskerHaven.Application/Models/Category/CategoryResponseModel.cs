using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Models.Category
{
    public record class CategoryResponseModel(int Id, string Name, string? Description);

}
