using Ecommerce.APPLICATION.Features.Categories.Commands.CreateCategory;
using Ecommerce.APPLICATION.Features.Categories.Commands.DeleteCategory;
using Ecommerce.APPLICATION.Features.Categories.Commands.UpdateCategory;
using Ecommerce.APPLICATION.Features.Categories.Queries.GetAllCategories;
using Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing product categories
/// </summary>
[ApiController]
[Route("api/v1/categories")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all categories with pagination
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="activeOnly">Filter by active status</param>
    /// <returns>Paged list of categories</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCategories(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool? activeOnly = null)
    {
        var query = new GetAllCategoriesQuery(pageNumber, pageSize, activeOnly);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <returns>Category details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    /// <param name="command">Category creation details</param>
    /// <returns>Created category ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(
            nameof(GetCategoryById),
            new { id = result.Value },
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing category
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <param name="command">Category update details</param>
    /// <returns>No content on success</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.CategoryId)
            return BadRequest(new { error = "Category ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "Category updated successfully" });
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
