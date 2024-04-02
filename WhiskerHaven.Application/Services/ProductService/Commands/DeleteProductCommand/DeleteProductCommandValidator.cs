using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.ProductService.Commands.DeleteProductCommand
{
    /// <summary>
    /// Represents a validator for the DeleteProductCommand.
    /// </summary>
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandValidator"/> class.
        /// </summary>
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Continue)
                .NotNull().
                NotEmpty().WithMessage("{PropertyName} no puede estar vacío");
        }
    }
}
