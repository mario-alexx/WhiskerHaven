using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Product;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Products
{
    public partial class CompProducts
    {
        public IEnumerable<ProductResponseModel> Products { get; set; } = new List<ProductResponseModel>();
        private bool Process { get; set; } = false;
        private int? DeleteProductId { get; set; } = null;

        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await productService.GetProducts();
        }

        public async Task HandlerOnDelete(int productId)
        {
            DeleteProductId = productId;
            await JSRuntime.InvokeVoidAsync("ShowModalConfirmDelete");
        }

        public async Task ClickConfirmDelete(bool confirm)
        {
            Process = true;
            if(confirm && DeleteProductId != null)
            {
                await productService.DeleteProduct(DeleteProductId.Value);
                await JSRuntime.ToastrSuccess("Product delete successfuly");
                Products = await productService.GetProducts();
            }
            await JSRuntime.InvokeVoidAsync("HideModalConfirmDelete");
            Process = false;
        }
    }
}
