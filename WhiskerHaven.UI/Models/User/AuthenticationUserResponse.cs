namespace WhiskerHaven.UI.Models.User
{
    public class AuthenticationUserResponse
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public UserModel User { get; set; }
    }
}
