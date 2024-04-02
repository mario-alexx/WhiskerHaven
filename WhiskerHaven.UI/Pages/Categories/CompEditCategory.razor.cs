using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Categories
{
    public partial class CompEditCategory
    {
        private CategoryModel EditCategory { get; set; } = new CategoryModel();
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditCategory = await categoryService.GetCategoryById(Id.Value);
        }

        private async Task HandlerOnEditCategory()
        {
            await categoryService.UpdateCategory(Id.Value, EditCategory);
            await JSRuntime.ToastrSuccess("Successfully category modified");
            navigationManager.NavigateTo("categories");
        }

    }
}
