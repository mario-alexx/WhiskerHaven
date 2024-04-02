using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models;
using WhiskerHaven.Application.Services.JwtService;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.UserService.SignIn
{
    /// <summary>
    /// Represents a command to sign in a user.
    /// </summary>
    public record class SignInCommand(string Email, string Password) : IRequest<UserLoginResponse>;

    /// <summary>
    /// Represents a handler for the SignInCommand.
    /// </summary>
    public class SignInCommandHandler : IRequestHandler<SignInCommand, UserLoginResponse>
    {
        /// <summary>
        /// The unit of work for database operations.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The service for JWT token generation.
        /// </summary>
        private readonly IJwtService _jwtService;

        /// <summary>
        /// The mapper for object mappings.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the SignInCommandHandler class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="jwtService">The service for JWT token generation.</param>
        /// <param name="mapper">The mapper for object mappings.</param>
        public SignInCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the sign-in command asynchronously.
        /// </summary>
        /// <param name="request">The sign-in command.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation that returns the user login response.</returns>
        public async Task<UserLoginResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // Get user by email and password
            var login = await _unitOfWork.UserRepository.LoginAsync(request.Email, request.Password);

            // If user login fails, return null
            if (login == null)
            {
                return null;
            }
            // Map request to user object
          //  var user = _mapper.Map<User>(request);

            // Generate JWT token
            var userLoginResponse = _jwtService.Generate(login);

            // Return the user login response containing the JWT token
            return userLoginResponse;
        }
    }
}
