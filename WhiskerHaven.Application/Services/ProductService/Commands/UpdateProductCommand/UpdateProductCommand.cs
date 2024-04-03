using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;

namespace WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand
{
    /// <summary>
    /// Represents a command to update a product.
    /// </summary>
    public record class UpdateProductCommand(int Id, string Name, string Description, int Stock, decimal Price, int CategoryId, string UrlImage) : IRequest<bool>;

    /// <summary>
    /// Represents a handler for the UpdateProductCommand.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
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
        /// Constructor for the UpdateProductCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to update a product asynchronously.
        /// </summary>
        /// <param name="request">The command to update a product.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns a boolean indicating whether the product was successfully updated.</returns>
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the product by its identifier asynchronously
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            // If the product is not found, return false
            if (result == null)
            {
                return false;
            }

            // Map the properties from the command to the retrieved product
            var product = _mapper.Map(request, result);

            // Update the product in the repository
            _unitOfWork.ProductsRepository.Update(product);

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
