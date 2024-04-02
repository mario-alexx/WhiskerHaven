using AutoMapper;
using WhiskerHaven.UI.Models.Category;
namespace WhiskerHaven.UI.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryModel, DropDownCategory>().ReverseMap();
        }
    }
}
