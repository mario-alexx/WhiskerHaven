using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Models.Product;

namespace WhiskerHaven.UI.Services.IService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryModel>> GetCategories();
        public Task<CategoryModel> GetCategoryById(int categoryId);
        public Task<CategoryModel> AddCategory(CategoryModel category);
        public Task<CategoryModel> UpdateCategory(int categoryId, CategoryModel category);
        public Task<bool> DeleteCategory(int categoryId);
    }
}
