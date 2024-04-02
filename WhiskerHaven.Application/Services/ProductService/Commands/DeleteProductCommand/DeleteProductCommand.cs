using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.ProductService.Commands.DeleteProductCommand
{
    /// <summary>
    /// Represents a command to delete a product by its identifier.
    /// </summary>
    public record class DeleteProductCommand(int Id) : IRequest<bool>;

    /// <summary>
    /// Represents a handler for the DeleteProductCommand.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        /// <summary>
        /// The unit of work for database operations.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the DeleteProductCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete a product by its identifier asynchronously.
        /// </summary>
        /// <param name="request">The command to delete a product by its identifier.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns a boolean indicating whether the product was successfully deleted.</returns>
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the product by its identifier asynchronously
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            // If the product is not found, return false
            if (result == null)
            {
                return false;
            }

            // Delete the product from the repository
            _unitOfWork.ProductRepository.Delete(result);
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
