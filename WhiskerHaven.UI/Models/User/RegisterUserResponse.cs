namespace WhiskerHaven.UI.Models.User
{
    public class RegisterUserResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public bool CorrectRegister { get; set; }
    }
}
