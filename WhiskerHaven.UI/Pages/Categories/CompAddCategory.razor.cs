using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.Category;
using WhiskerHaven.UI.Services.IService;
using WhiskerHaven.UI.Services;

namespace WhiskerHaven.UI.Pages.Categories
{

    public partial class CompAddCategory
    {
        private CategoryModel AddCategory { get; set; } = new CategoryModel();

        [Inject]
        public ICategoryService categoryService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task HandlerOnAddCategory()
        {
            var addcategory = await categoryService.AddCategory(AddCategory);
            await JSRuntime.ToastrSuccess("Successfully category added");
            navigationManager.NavigateTo("categories");
        }
    }
}
