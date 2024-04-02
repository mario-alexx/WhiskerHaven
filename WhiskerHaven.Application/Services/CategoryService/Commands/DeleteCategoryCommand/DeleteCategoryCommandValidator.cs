using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Services.CategoryService.Commands.DeleteCategoryCommand
{
    /// <summary>
    /// Validator for the DeleteCategoryCommand.
    /// </summary>
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryCommandValidator"/> class.
        /// </summary>
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío");
        }
    }
}
