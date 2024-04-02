using WhiskerHaven.UI.Models.User;

namespace WhiskerHaven.UI.Services.IService
{
    public interface IAuthenticationService
    {
        Task<RegisterUserResponse> SignUp(RegisterUser user);
        Task<AuthenticationUserResponse> SignIn(AuthenticateUser user);
        Task Logout();
    }
}
