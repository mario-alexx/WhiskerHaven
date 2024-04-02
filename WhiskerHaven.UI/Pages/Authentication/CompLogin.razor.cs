using Microsoft.AspNetCore.Components;
using System.Web;
using WhiskerHaven.UI.Models.User;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Authentication
{
    public partial class CompLogin
    {
        private AuthenticateUser userAuth = new ();
        public bool Process { get; set; } = false;
        public bool ShowErrorsAuthentication { get; set; }
        public string ReturnUrl { get; set; }

        public string Errors { get; set; }

        [Inject]
        public IAuthenticationService serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task UserAccess()
        {
            ShowErrorsAuthentication = false;
            Process = true;
            var result = await serviceAuthentication.SignIn(userAuth);

            if (result.IsSuccess)
            {
                Process = false;
                var absoluteUrl = new Uri(navigationManager.Uri);
                var queryParams = HttpUtility.ParseQueryString(absoluteUrl.Query);
                ReturnUrl = queryParams["returnUrl"];

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
                Process = false;
                ShowErrorsAuthentication = true;
                Errors = "The username or password is not valid";
                navigationManager.NavigateTo("/signin");
            }
        }
    }
}
