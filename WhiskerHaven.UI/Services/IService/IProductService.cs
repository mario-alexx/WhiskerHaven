using WhiskerHaven.UI.Models.Product;

namespace WhiskerHaven.UI.Services.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponseModel>> GetProducts();
        public Task<ProductResponseModel> GetProductById(int id);
        public Task<ProductResponseModel> AddProduct(ProductRequestModel product);
        public Task<ProductResponseModel> UpdateProduct(int productId, ProductResponseModel product);
        public Task<bool> DeleteProduct(int productId);
        public Task<string> UploadImage(MultipartFormDataContent content);
    }
}
