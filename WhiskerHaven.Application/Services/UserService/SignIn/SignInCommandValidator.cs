using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.UserService.SignIn
{
    /// <summary>
    /// Represents a validator for the SignInCommand.
    /// </summary>
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        /// <summary>
        /// Constructor for the SignInCommandValidator class.
        /// </summary>
        public SignInCommandValidator()
        {
            RuleFor(e => e.Email).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El email es obligatorio")
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Ingrese un email valido");

            RuleFor(p => p.Password).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El password es obligatorio")
                .NotEmpty().WithMessage("El password es obligatorio")
                .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,}$").WithMessage("El password debe contener minimo 1 mayúscula,1 minúscula, 1 número y un carácter especial");
        }
    }
}
