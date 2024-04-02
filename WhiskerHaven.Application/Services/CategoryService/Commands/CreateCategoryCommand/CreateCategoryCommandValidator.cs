using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.CategoryService.Commands.CreateCategoryCommand
{
    /// <summary>
    /// Validator for the CreateCategoryCommand.
    /// </summary>
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryCommandValidator"/> class.
        /// </summary>
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("Debe ingresar un nombre")
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50).WithMessage("El nombre debe contener menos de 50 caracteres")
                .MinimumLength(4).WithMessage("El nombre debe contener al menos 4 caracteres");
            RuleFor(x => x.Description).Cascade(CascadeMode.Continue)
                .NotNull().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("La descripción debe contener menos de 50 caracteres")
                .MinimumLength(4).WithMessage("La descripción debe contener al menos 4 caracteres");
        }
    } 
}
