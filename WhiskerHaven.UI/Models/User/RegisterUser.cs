using System.ComponentModel.DataAnnotations;

namespace WhiskerHaven.UI.Models.User
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(4, ErrorMessage = "El nombre debe contener minimo 4 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre debe contener menos de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Debe ingresar un nombre valido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MinLength(4, ErrorMessage = "El apellido debe contener minimo 4 caracteres")]
        [MaxLength(50, ErrorMessage = "El apellido debe contener menos de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Debe ingresar un apellido valido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,}$", ErrorMessage = "El password debe contener minimo 1 mayúscula,1 minúscula, 1 número y un carácter especial")]
        public string Password { get; set; }

        [RegularExpression("^\\+(?:\\d\\s?){6,14}\\d$", ErrorMessage = "Ingrese un número de teléfono válido")]
        public string PhoneNumber { get; set; }
    }
}
