using Microsoft.AspNetCore.Components;
using System.Web;
using WhiskerHaven.UI.Models.User;
using WhiskerHaven.UI.Services.IService;

namespace WhiskerHaven.UI.Pages.Authentication
{
    public partial class CompRegister
    {
        private RegisterUser userRegister = new();
        public bool Process { get; set; } = false;
        public bool ShowErrorsRegister { get; set; }

        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task RegisterNewUser()
        {
            ShowErrorsRegister = false;
            Process = true;
            var result = await serviceAuthentication.SignUp(userRegister);

            if (result.CorrectRegister)
            {
                Process = false;
                navigationManager.NavigateTo("/signin");
            }
            else
            {
                Process = false;
                Errors = result.Errors;
                ShowErrorsRegister = true;
            }
        }
    }
}
