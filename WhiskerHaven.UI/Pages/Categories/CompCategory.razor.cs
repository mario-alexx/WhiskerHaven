using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Models.Product;
using WhiskerHaven.UI.Services;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Categories
{
    public partial  class CompCategory
    {
        public IEnumerable<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        private bool Process { get; set; } = false;
        private int? DeleteCategoryId { get; set; } = null;

        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categories = await categoryService.GetCategories();
        }

        public async Task HandlerOnDelete(int categoryId)
        {
            DeleteCategoryId = categoryId;
            await JSRuntime.InvokeVoidAsync("ShowModalConfirmDelete");
        }

        public async Task ClickConfirmDelete(bool confirm)
        {
            Process = true;
            if (confirm && DeleteCategoryId != null)
            {
                await categoryService.DeleteCategory(DeleteCategoryId.Value);
                await JSRuntime.ToastrSuccess("Product delete successfuly");
                Categories = await categoryService.GetCategories();
            }
            await JSRuntime.InvokeVoidAsync("HideModalConfirmDelete");
            Process = false;
        }
    }
}
