using AutoMapper;
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

namespace WhiskerHaven.Application.Services.ProductService.Commands.CreateProductCommand
{
    /// <summary>
    /// Represents a command to create a new product.
    /// </summary>
    public record class CreateProductCommand(string Name, string Description, int Stock, decimal Price, int CategoryId, string UrlImage) : IRequest<AddProductResponseModel>;

    /// <summary>
    /// Represents a handler for the CreateProductCommand.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, AddProductResponseModel>
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
        /// Constructor for the CreateProductCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to create a new product asynchronously.
        /// </summary>
        /// <param name="request">The command to create a new product.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns the added product response model.</returns>
        public async Task<AddProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Map the command to a Product entity
            var product = _mapper.Map<Product>(request);

            // Add the product to the repository
            _unitOfWork.ProductRepository.Add(product);

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AddProductResponseModel>(product);

        }
    }
}
