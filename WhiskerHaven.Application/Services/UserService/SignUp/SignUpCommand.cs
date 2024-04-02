using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.UserService.SignUp
{
    /// <summary>
    /// Represents a command to sign up a new user.
    /// </summary>
    public record class SignUpCommand(string Name, string LastName, string Email, string Password, string PhoneNumber) : IRequest<bool>;

    /// <summary>
    /// Represents a handler for the SignUpCommand.
    /// </summary>
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
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
        /// Constructor for the SignUpCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public SignUpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the sign-up command asynchronously.
        /// </summary>
        /// <param name="request">The sign-up command.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns true if the sign-up was successful; otherwise, false.</returns>

        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            // Check if the user already exists
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                // Map the command to a User entity
                var result = _mapper.Map<User>(request);
                // Register the new user
                _unitOfWork.UserRepository.Register(result);
                // Save changes to the database
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }            
            return false;
        }
    }
}
