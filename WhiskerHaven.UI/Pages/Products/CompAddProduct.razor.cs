using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Models.Product;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Products
{
    public partial class CompAddProduct
    {
        private ProductRequestModel AddProduct { get; set; } = new ProductRequestModel();
        private DropDownCategory selectedCategory = new();

        //private IEnumerable<DropDownCategory> dropDownCategories { get; set; } = new List<DropDownCategory>();
        private IEnumerable<CategoryModel> dropDownCategories { get; set; } = new List<CategoryModel>();

        [Parameter]
        public string ProductImage { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public ICategoryService categoryService { get; set; }

        public IMapper _mapper;
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //var allCategories = await categoryService.GetCategories();
            //var result = _mapper.Map<IEnumerable<DropDownCategory>>(allCategories);
            //dropDownCategories = result;

            //dropDownCategories =  _mapper.Map<IEnumerable<DropDownCategory>>(await categoryService.GetCategories());

            dropDownCategories = await categoryService.GetCategories();
            
        }

        private async Task HandlerOnAddProduct()
        {
            AddProduct.UrlImage = ProductImage;
            AddProduct.CategoryId = selectedCategory.Id;
            var addproduct = await productService.AddProduct(AddProduct);
            await JSRuntime.ToastrSuccess("Successfully product added");
            navigationManager.NavigateTo("products");
        }

        private async Task HandlerOnUploadFile(InputFileChangeEventArgs e)
        {
            IBrowserFile imageFile = e.File;

            if(imageFile != null)
            {
                var resizedFile = await imageFile.RequestImageFileAsync("image/png", 1000, 700);

                using(Stream ms = resizedFile.OpenReadStream(resizedFile.Size)) 
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
