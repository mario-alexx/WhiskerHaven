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

namespace WhiskerHaven.Application.Services.CategoryService.Queries
{
    /// <summary>
    /// Represents a query to retrieve all categories.
    /// </summary>
    public record class GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryResponseModel>>;

    /// <summary>
    /// Represents a handler for the GetAllCategoriesQuery.
    /// </summary>
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseModel>>
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
        /// Constructor for the GetAllCategoriesQueryHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve all categories asynchronously.
        /// </summary>
        /// <param name="request">The query to retrieve all categories.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns a collection of category response models.</returns>
        public async Task<IEnumerable<CategoryResponseModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all categories asynchronously from the repository
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            // Map the retrieved categories to category response models
            return _mapper.Map<IEnumerable<CategoryResponseModel>>(categories);
        }
    }
}
