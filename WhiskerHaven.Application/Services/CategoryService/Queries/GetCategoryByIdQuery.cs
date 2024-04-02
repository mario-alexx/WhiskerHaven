using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.CategoryService.Queries
{
    /// <summary>
    /// Represents a query to retrieve a category by its identifier.
    /// </summary>
    public record class GetCategoryByIdQuery(int Id) : IRequest<CategoryResponseModel>;

    /// <summary>
    /// Represents a handler for the GetCategoryByIdQuery.
    /// </summary>
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseModel>
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
        /// Constructor for the GetCategoryByIdQueryHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a category by its identifier asynchronously.
        /// </summary>
        /// <param name="request">The query to retrieve a category by its identifier.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns a category response model.</returns>
        public async Task<CategoryResponseModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the category by its identifier asynchronously from the repository
            var categoryId = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            // If the category is not found, return null
            if (categoryId == null)
            {
                return null;
            }

            return _mapper.Map<CategoryResponseModel>(categoryId);
        }
    }
}
