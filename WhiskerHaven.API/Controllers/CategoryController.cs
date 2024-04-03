using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhiskerHaven.Application.Models.Category;
using WhiskerHaven.Application.Services.CategoryService.Commands.CreateCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Commands.DeleteCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Commands.UpdateCategoryCommand;
using WhiskerHaven.Application.Services.CategoryService.Queries;
using WhiskerHaven.Application.Services.ProductService.Commands.CreateProductCommand;
using WhiskerHaven.Application.Services.ProductService.Commands.UpdateProductCommand;

namespace WhiskerHaven.API.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        /// <summary>
        /// Constructor for CategoryController class.
        /// </summary>
        /// <param name="mediator">Instance of IMediator to handle commands.</param>
        /// <param name="logger">Instance of ILogger to log information.</param>
        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Method to retrieve all categories.
        /// </summary>
        /// <returns>HTTP response containing the list of categories.</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all categories.");

            var result =  await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Method to retrieve a category by ID.
        /// </summary>
        /// <param name="id">ID of the category to retrieve.</param>
        /// <returns>HTTP response containing the category details.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Fetching category by ID: {id}");

            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            if(result == null)
            {
                _logger.LogWarning($"Category with ID {id} not found.");
                return NotFound("The category to be fetched was not found.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Method to add a new category.
        /// </summary>
        /// <param name="request">Data for creating the new category.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(CreateCategoryCommand))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand request)
        {
            _logger.LogInformation($"Adding a new category: {request.Name}: {request.Description}");
            
            var createCategoryValidator = new CreateCategoryCommandValidator();
            ValidationResult resultValidation = createCategoryValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select(x => new { x.PropertyName, x.ErrorMessage}) );
            }

            var category = await _mediator.Send(request);
            _logger.LogInformation($"New category added successfull: {request.Name}");
            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, category);
            //return Ok(category);
        }

        /// <summary>
        /// Method to update an existing category.
        /// </summary>
        /// <param name="id">ID of the category to update.</param>
        /// <param name="request">Data for updating the category.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(UpdateCategoryCommand))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand request)
        {
            _logger.LogInformation($"Updating category with ID: {request.Id}: {request.Name}");
           
            if(id != request.Id)
            {
                _logger.LogWarning("ID in the request body does not match the ID in the URL.");
                return BadRequest();
            }

            var updateCategoryValidator = new UpdateCategoryCommandValidator();
            ValidationResult resultValidation = updateCategoryValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select(x => new { x.PropertyName, x.ErrorMessage} ));
            }

            var result = await _mediator.Send(request);

            if (!result)
            {
                _logger.LogWarning($"Category with ID {request.Id}: {request.Name} not found for update.");
                NotFound("The category to be updated was not found.");
            }

            _logger.LogInformation($"Category updated successfully. ID: {request.Id}");
            return NoContent();
        }

        /// <summary>
        /// Method to delete a category.
        /// </summary>
        /// <param name="id">ID of the category to delete.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting category with ID: {id}");

            var deleteCategoryValidator = new DeleteCategoryCommandValidator();
            ValidationResult resultValidation = deleteCategoryValidator.Validate(new DeleteCategoryCommand(id));

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors.Select( x => new { x.PropertyName, x.ErrorMessage} ));
            }

            var result = await _mediator.Send(new DeleteCategoryCommand(id));

            if (!result)
            {
                _logger.LogWarning($"Category with ID {id} not found for deletion.");
                NotFound("The category to be deleted was not found.");
            }

            _logger.LogInformation($"Category deleted successfully. ID: {id}");
            return NoContent();
        }
    }
}
