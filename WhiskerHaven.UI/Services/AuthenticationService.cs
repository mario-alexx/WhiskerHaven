using Blazored.LocalStorage;
using WhiskerHaven.UI.Helpers;
using WhiskerHaven.UI.Models.User;
using WhiskerHaven.UI.Services.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace WhiskerHaven.UI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthenticationUserResponse> SignIn(AuthenticateUser userAuth)
        {
            string content = JsonConvert.SerializeObject(userAuth);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/account/login", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                //var token = result["result"]["token"].Value<string>();
                //var user = result["result"]["user"]["email"].Value<string>();
                var token = result["token"].Value<string>();
                var user = result["user"]["email"].Value<string>();

                await _localStorageService.SetItemAsync(Initialize.Token_Local, token);
                await _localStorageService.SetItemAsync(Initialize.Data_User_Local, user);
                ((AuthStateProvider)_authenticationStateProvider).NotificateUserLogin(token);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                return new AuthenticationUserResponse { IsSuccess = true };
            }
            else
            {
                return new AuthenticationUserResponse { IsSuccess = false };
            }
        }

        public async Task<RegisterUserResponse> SignUp(RegisterUser user)
        {
            string content = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/account/register", bodyContent);

            string contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterUserResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegisterUserResponse { CorrectRegister = true};
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(Initialize.Token_Local);
            await _localStorageService.RemoveItemAsync(Initialize.Data_User_Local);

            ((AuthStateProvider)_authenticationStateProvider).NotificateUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
