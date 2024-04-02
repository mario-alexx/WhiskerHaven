using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using WhiskerHaven.Application.Models;
using WhiskerHaven.Application.Services.UserService.SignIn;
using WhiskerHaven.Application.Services.UserService.SignUp;

namespace WhiskerHaven.API.Controllers
{
    /// <summary>
    /// Controller for user account management.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        protected ApiResponse _apiResponse;

        /// <summary>
        /// Constructor for AccountController class.
        /// </summary>
        /// <param name="mediator">Instance of IMediator to handle command requests.</param>
        /// <param name="logger">Instance of ILogger to log information.</param>
        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _apiResponse = new();
        }

        /// <summary>
        /// Method to register a new user.
        /// </summary>
        /// <param name="request">User registration data.</param>
        /// <returns>HTTP response with operation result.</returns>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SignUpCommand request) 
        {
            _logger.LogInformation($"Reistering new user: {@request.Email} - {request.PhoneNumber}");
           
            var userValidator = new SignUpCommandValidator();
            ValidationResult validationResult = userValidator.Validate(request);
            
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage}));
            }

            bool result = await _mediator.Send(request);

            if (!result)
            {
                _logger.LogWarning($"User: {request}  already exists");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage.Add("User already exists.");
                return BadRequest(_apiResponse);
            }
            _logger.LogInformation($"User created successfully: {@request.Email} - {request.PhoneNumber}");
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);  
        }

        /// <summary>
        /// Method to log in a user.
        /// </summary>
        /// <param name="request">User login data.</param>
        /// <returns>HTTP response with operation result.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] SignInCommand request)
        {
            _logger.LogInformation($"Logging in: {@request.Email}");

            var userValidator = new SignInCommandValidator();
            ValidationResult validationResult = userValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage}));
            }

            UserLoginResponse userLogin = await _mediator.Send(request);

            if(userLogin == null)
            {
                _logger.LogWarning($"Error logging in: Incorrect email or password. User: {@request.Email}");

                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage.Add("Incorrect email or password.");
                return BadRequest(_apiResponse);
            }

            _logger.LogInformation($"Login successful: {request.Email}");
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = userLogin;
            return Ok(userLogin);  
        }
    } 
}
