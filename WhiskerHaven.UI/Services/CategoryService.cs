using Newtonsoft.Json;
using System.Net;
using System.Text;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            HttpResponseMessage response = await _client.GetAsync($"{Initialize.UrlBaseApi}api/category");
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<CategoryModel> categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(content);
            return categories;
            
        }
        public async Task<CategoryModel> GetCategoryById(int categoryId)
        {
            HttpResponseMessage response = await _client.GetAsync($"{Initialize.UrlBaseApi}api/category/{categoryId}");
            string content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                CategoryModel category = JsonConvert.DeserializeObject<CategoryModel>(content);
                return category;
            }
            else
            {
                ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<CategoryModel> AddCategory(CategoryModel category)
        {
            string  content = JsonConvert.SerializeObject(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/category", bodyContent);
            string contentTemp = await response.Content.ReadAsStringAsync();   

            if(response.IsSuccessStatusCode)
            {
                CategoryModel result = JsonConvert.DeserializeObject<CategoryModel>(contentTemp);
                return result;
            }
            else
            {
                ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<CategoryModel> UpdateCategory(int categoryId, CategoryModel category)
        {
            string content = JsonConvert.SerializeObject(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{Initialize.UrlBaseApi}api/category/{categoryId}", bodyContent);
            string contentTemp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                CategoryModel result = JsonConvert.DeserializeObject<CategoryModel>(contentTemp);
                return result;
            }
            else
            {
                ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{Initialize.UrlBaseApi}api/category/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
