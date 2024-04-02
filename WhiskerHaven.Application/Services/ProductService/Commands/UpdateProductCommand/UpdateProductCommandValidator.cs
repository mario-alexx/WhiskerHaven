using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand
{
    /// <summary>
    /// Represents a validator for the UpdateProductCommand.
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class.
        /// </summary>
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Continue)
                .NotNull()
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50).WithMessage("El nombre debe contener menos de 50 caracteres")
                .MinimumLength(4).WithMessage("El nombre debe contener al menos 4 caracteres")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Debe ingresar un nombre valido");

            RuleFor(x => x.Description).Cascade(CascadeMode.Continue)
                .NotNull()
                .NotEmpty().WithMessage("La descripción es obligatoria")
                .MaximumLength(120).WithMessage("La descripción debe contener menos de 100 caracteres")
                .MinimumLength(4).WithMessage("La descripción debe contener al menos 4 caracteres");

            RuleFor(x => x.Stock).Cascade(CascadeMode.Continue)
                .NotNull()
                .NotEmpty().WithMessage("El stock es obligatorio")
                .GreaterThan(0).WithMessage("El stock debe ser mayor a 0");

            RuleFor(x => x.Price).Cascade(CascadeMode.Continue)
                .NotNull()
                .NotEmpty().WithMessage("La descripción es obligatoria")
                .PrecisionScale(10, 2, false).WithMessage("El precio solo puede tener 2 decimales")
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
        }
    }
}
