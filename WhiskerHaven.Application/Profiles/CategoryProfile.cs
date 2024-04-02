using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Application.Services.CategoryService.Commands.CreateCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Commands.UpdateCategoryCommand;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Profiles
{
    /// <summary>
    /// Represents a mapping profile for category-related operations.
    /// </summary>
    public class CategoryProfile : Profile
    {
        /// <summary>
        /// Constructor for the CategoryProfile class.
        /// </summary>
        public CategoryProfile()
        {
            // Maps from CreateCategoryCommand to Category and vice versa.
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();

            // Maps from UpdateCategoryCommand to Category and vice versa.
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();

            // Maps from Category to CategoryResponseModel and vice versa.
            CreateMap<Category, CategoryResponseModel>().ReverseMap();
        }
    }
}
