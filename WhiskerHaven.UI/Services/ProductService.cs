using Newtonsoft.Json;
using System.Text;
using System.Net;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models;
using WhiskerHaven.UI.Models.Product;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductResponseModel>> GetProducts()
        {
            HttpResponseMessage response = await _client.GetAsync($"{Initialize.UrlBaseApi}api/product");
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<ProductResponseModel> products = JsonConvert.DeserializeObject<IEnumerable<ProductResponseModel>>(content);
            return products;
        }

        public async Task<ProductResponseModel> GetProductById(int productId)
        {
            HttpResponseMessage response = await _client.GetAsync($"{Initialize.UrlBaseApi}api/product/{productId}");
            string content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                ProductResponseModel product = JsonConvert.DeserializeObject<ProductResponseModel>(content);
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<ProductResponseModel> AddProduct(ProductRequestModel product)
        {
            string content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/product", bodyContent);
            string contentTemp = await response.Content.ReadAsStringAsync();    

            if(response.IsSuccessStatusCode)
            {
                ProductResponseModel result = JsonConvert.DeserializeObject<ProductResponseModel>(contentTemp);
                return result;
            }
            else
            {
                ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
        public async Task<ProductResponseModel> UpdateProduct(int productId, ProductResponseModel product)
        {
            string content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{Initialize.UrlBaseApi}api/product/{productId}", bodyContent);
            string contentTemp = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ProductResponseModel>(contentTemp);
                return result;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            } 
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{Initialize.UrlBaseApi}api/product/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return true;    
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }            
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            HttpResponseMessage response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/upload", content);
            string productContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(productContent);
            }
            else
            {
                string imageProduct = Path.Combine($"{Initialize.UrlBaseApi}", productContent);
                return imageProduct;
            }
        }
    }
}
