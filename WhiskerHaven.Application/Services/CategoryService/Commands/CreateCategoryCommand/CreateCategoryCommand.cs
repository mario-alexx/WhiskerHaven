using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Application.Models.Product;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.CategoryService.Commands.CreateCategoryCommand
{
    /// <summary>
    /// Represents a command to create a new category.
    /// </summary>
    public record class CreateCategoryCommand(string Name, string Description) : IRequest<CategoryResponseModel>;

    /// <summary>
    /// Represents a handler for the CreateCategoryCommand.
    /// </summary>
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponseModel>
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
        /// Constructor for the CreateCategoryCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to create a new category asynchronously.
        /// </summary>
        /// <param name="request">The command to create a new category.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns a category response model.</returns>
        public async Task<CategoryResponseModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Map the command to a category entity
            var category = _mapper.Map<Category>(request);

            // Add the category to the repository and save changes asynchronously
            _unitOfWork.CategoryRepository.Add(category);

            // Map the created category to a category response model and return
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryResponseModel>(category);
        }
    }
}
