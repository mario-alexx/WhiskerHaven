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
    /// Represents a query to retrieve a product by its identifier.
    /// </summary>
    public record class GetProductByIdQuery(int Id) : IRequest<ProductResponseModel>;

    /// <summary>
    /// Represents a handler for the GetProductByIdQuery.
    /// </summary>
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseModel>
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
        /// Constructor for the GetProductByIdQueryHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a product by its identifier asynchronously.
        /// </summary>
        /// <param name="request">The query to retrieve a product by its identifier.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns the product response model.</returns>
        public async Task<ProductResponseModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the product by its identifier asynchronously
            var productId = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            // If the product is not found, return null
            if (productId == null)
            {
                return null;
            }

            // Map the product to a product response model
            return _mapper.Map<ProductResponseModel>(productId);
        }
    }
}
