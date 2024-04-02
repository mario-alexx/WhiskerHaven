using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.CategoryService.Commands.UpdateCategoryCommand
{
    /// <summary>
    /// Represents a command to update a category.
    /// </summary>
    public record class UpdateCategoryCommand(int Id, string Name, string? Description) : IRequest<bool>;

    /// <summary>
    /// Handles the updating of a category.
    /// </summary>
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        /// <summary>
        /// The unit of work for database operations.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The mapper for object mappings.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance.</param>
        /// <param name="mapper">The mapper instance.</param>
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the updating of a category.
        /// </summary>
        /// <param name="request">The update category command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the category by its identifier
            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            // If the category doesn't exist, return false
            if (result == null)
            {
                return false;
            }
            // Map the properties from the command to the retrieved category entity
            var category = _mapper.Map(request, result);

            // Update the category
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
