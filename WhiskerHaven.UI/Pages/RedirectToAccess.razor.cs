using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace WhiskerHaven.UI.Pages
{
    public partial class RedirectToAccess
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationProviderStatus { get; set; }
        bool NotAuthorize { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authorizationStatus = await AuthenticationProviderStatus;

            if (authorizationStatus.User == null)
            {
                string returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    NavigationManager.NavigateTo("Access", true);
                }
                else
                {
                    NavigationManager.NavigateTo($"Access?returnUrl={returnUrl}", true);
                }
            } 
            else
            {
                NotAuthorize = true;
            }

        }
    }
}
