using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.UserService.SignUp
{
    /// <summary>
    /// Represents a validator for the SignUpCommand.
    /// </summary>
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        /// <summary>
        /// Constructor for the SignUpCommandValidator class.
        /// </summary>
        public SignUpCommandValidator()
        {
            RuleFor(e => e.Email).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El email es obligatorio")
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Ingrese un email valido");

            RuleFor(p => p.Password).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El password es obligatorio")
                .NotEmpty().WithMessage("El password es obligatorio")
                .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,}$").WithMessage("El password debe contener minimo 1 mayúscula,1 minúscula, 1 número y un carácter especial");

            RuleFor(n => n.Name).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El email es obligatorio")
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50).WithMessage("El nombre debe contener menos de 50 caracteres")
                .MinimumLength(4).WithMessage("El nombre debe contener minimo 4 caracteres")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Debe ingresar un nombre valido")
                .Must(BeAvalidName).WithMessage("Ingrese un nombre válido");

            RuleFor(a => a.LastName).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El apellido es obligatorio")
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(50).WithMessage("El apellido debe contener menos de 50 caracteres")
                .MinimumLength(4).WithMessage("El apellido debe contener minimo 4 caracteres")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Debe ingresar un apellido valido")
                .Must(BeAvalidName).WithMessage("Ingrese un apellido válido");


            RuleFor(t => t.PhoneNumber).Cascade(CascadeMode.Continue)
                .Matches("^\\+(?:\\d\\s?){6,14}\\d$").WithMessage("Ingrese un número de teléfono válido");
        }

        /// <summary>
        /// Validates whether the provided name is valid.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>True if the name is valid; otherwise, false.</returns>
        private bool BeAvalidName(string name)
        {
            name = name.Trim();             
            name = name.Replace("_", "");   
            return name.All(char.IsLetter); 
        }
    }
}
