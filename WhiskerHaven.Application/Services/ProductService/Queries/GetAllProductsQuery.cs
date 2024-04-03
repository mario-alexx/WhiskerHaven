using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models.Product;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.ProductService.Queries
{
    /// <summary>
    /// Represents a query to retrieve all products.
    /// </summary>
    public record class GetAllProductsQuery() : IRequest<IEnumerable<ProductResponseModel>>;

    /// <summary>
    /// Represents a handler for the GetAllProductsQuery.
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseModel>>
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
        /// Constructor for the GetAllProductsQueryHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve all products asynchronously.
        /// </summary>
        /// <param name="request">The query to retrieve all products.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns the collection of product response models.</returns>

        public async Task<IEnumerable<ProductResponseModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all products asynchronously
            var products = await _unitOfWork.ProductsRepository.GetAllProductsAsync();

            // Map the products to product response models
            return _mapper.Map<IEnumerable<ProductResponseModel>>(products);
        }
    }
}
