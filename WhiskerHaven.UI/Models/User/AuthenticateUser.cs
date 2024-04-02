using System.ComponentModel.DataAnnotations;

namespace WhiskerHaven.UI.Models.User
{
    public class AuthenticateUser 
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,}$", ErrorMessage = "El password debe contener minimo 1 mayúscula,1 minúscula, 1 número y un carácter especial")]
        public string Password { get; set; }
    }

}
