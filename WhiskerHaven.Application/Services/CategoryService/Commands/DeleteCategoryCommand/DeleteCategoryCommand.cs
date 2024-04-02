using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.CategoryService.Commands.DeleteCategoryCommand
{
    /// <summary>
    /// Represents a command to delete a category.
    /// </summary>
    public record class DeleteCategoryCommand(int Id) : IRequest<bool>;

    /// <summary>
    /// Handles the deletion of a category based on its identifier.
    /// </summary>
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        /// <summary>
        /// The unit of work for database operations.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance.</param>
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the deletion of a category.
        /// </summary>
        /// <param name="request">The delete category command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the deletion is successful, otherwise false.</returns>
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the category by its identifier
            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            // If the category doesn't exist, return false
            if (result == null)
            {
                return false;
            }

            // Delete the category
            _unitOfWork.CategoryRepository.Delete(result);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
