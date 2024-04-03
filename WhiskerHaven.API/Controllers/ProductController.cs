using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WhiskerHaven.Application.Services.CategoryService.Commands.DeleteCategoryCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.CreateProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.DeleteProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand;
using WhiskerHaven.Application.Services.ProductService.Queries;

namespace WhiskerHaven.API.Controllers
{
    /// <summary>
    /// Controller for managing products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Constructor for ProductController class.
        /// </summary>
        /// <param name="mediator">Instance of IMediator to handle commands.</param>
        /// <param name="logger">Instance of ILogger to log information.</param>
        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Method to retrieve all products.
        /// </summary>
        /// <returns>HTTP response containing the list of products.</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all products.");
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Method to retrieve a product by ID.
        /// </summary>
        /// <param name="id">ID of the product to retrieve.</param>
        /// <returns>HTTP response containing the product details.</returns>
        [AllowAnonymous]
        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int productId)
        {
            _logger.LogInformation($"Fetching product by ID: {productId}");
            var result = await _mediator.Send(new GetProductByIdQuery(productId));

            if(result == null)
            {
                _logger.LogWarning($"Product with ID {productId} not found.");
                return NotFound("The product to be fetched was not found.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Method to add a new product.
        /// </summary>
        /// <param name="request">Data for creating the new product.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(CreateProductCommand))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand request)
        {
            _logger.LogInformation($"Adding a new product: {request.Name}: {request.Description}");

            var createProductValidator = new CreateProductCommandValidator();
            ValidationResult resultValidation = createProductValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select( x => new { x.PropertyName, x.ErrorMessage }));
            }

            var product = await _mediator.Send(request);
            if(product == null)
            {
                return BadRequest(product);
            }
            
            _logger.LogInformation($"New product added successfull: {request.Name}");
            return Ok(product);
        }

        /// <summary>
        /// Method to update an existing product.
        /// </summary>
        /// <param name="id">ID of the product to update.</param>
        /// <param name="request">Data for updating the product.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut("{productId}")]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(UpdateProductCommand))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int productId, [FromBody] UpdateProductCommand request)
        {
            _logger.LogInformation($"Updating product with ID: {request.Id}: {request.Name}");

            if (productId != request.Id)
            {
                _logger.LogWarning("ID in the request body does not match the ID in the URL.");
                return BadRequest();
            }

            var updateProductValidator = new UpdateProductCommandValidator();
            ValidationResult resultValidation = updateProductValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }

            var result = await _mediator.Send(request);

            if(!result)
            {
                _logger.LogWarning($"Product with ID {request.Id}: {request.Name} not found for update.");
                return NotFound("The product to be updated was not found.");
            }

            _logger.LogInformation($"Product updated successfully. ID: {request.Id}");
            return NoContent();
        }

        /// <summary>
        /// Method to delete a product.
        /// </summary>
        /// <param name="id">ID of the product to delete.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpDelete("{productId:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int productId)
        {
            _logger.LogInformation($"Deleting product with ID: {productId}");

            var deleteProductValidator = new DeleteProductCommandValidator();
            ValidationResult resultValidation = deleteProductValidator.Validate(new DeleteProductCommand(productId));

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }

            var result = await _mediator.Send(new DeleteCategoryCommand(productId));
            
            if(!result)
            {
                _logger.LogWarning($"Product with ID {productId} not found for deletion.");
                return NotFound("The product to be deleted was not found.");
            }

            _logger.LogInformation($"Product deleted successfully. ID: {productId}");
            return NoContent();
        }
        
    }
}
