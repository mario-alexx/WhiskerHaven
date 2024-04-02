using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Models.Product;
using WhiskerHaven.UI.Services;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Products
{
    public partial class CompEditProduct
    {
        private ProductRequestModel EditProductRequest { get; set; } = new ProductRequestModel();
        private ProductResponseModel EditProductResponse { get; set; } = new ProductResponseModel();
        private DropDownCategory selectedCategory = new();
        private IEnumerable<CategoryModel> dropDownCategories { get; set; } = new List<CategoryModel>();

        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public string ProductImage { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            dropDownCategories = await categoryService.GetCategories();

            //if(Id != null)   
            //{
            //    EditProductResponse = await productService.GetProductById(Id.Value);
            //    EditProductRequest = new ProductRequestModel
            //    {                  
            //        Name = EditProductResponse.Name,
            //        Description = EditProductResponse.Description,
            //        Stock = EditProductResponse.Stock,
            //        Price = EditProductResponse.Price,
            //        CategoryId = EditProductResponse.CategoryId,
            //        UrlImage = EditProductResponse.UrlImage
            //    };
            //}

            //if(Id != null)
            //{
                EditProductResponse = await productService.GetProductById(Id.Value);
            //}
        }

        private async Task HandlerOnEditProduct()
        {
            //EditProductRequest.CategoryId = selectedCategory.Id;
            //EditProductRequest.UrlImage = ProductImage;
            //await productService.UpdateProduct(Id.Value, EditProductRequest);
            //await JSRuntime.ToastrSuccess("Successfully product modified");
            //navigationManager.NavigateTo("products");

            EditProductResponse.CategoryId = selectedCategory.Id;
            EditProductResponse.UrlImage = ProductImage;
            await productService.UpdateProduct(Id.Value, EditProductResponse);
            await JSRuntime.ToastrSuccess("Successfully product modified");
            navigationManager.NavigateTo("products");
        }

        private async Task HandlerOnUploadFile(InputFileChangeEventArgs e)
        {
            IBrowserFile imageFile = e.File;

            if (imageFile != null)
            {
                var resizedFile = await imageFile.RequestImageFileAsync("image/png", 1000, 700);

                using (Stream ms = resizedFile.OpenReadStream(resizedFile.Size))
                {
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);

                    ProductImage = await productService.UploadImage(content);
                    await OnChange.InvokeAsync(ProductImage);
                }
            }
        }
    }
}
