using Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;
using Ecommerce.APPLICATION.Features.Products.Commands.DeleteProduct;
using Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;
using Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;
using Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;
using Ecommerce.APPLICATION.Features.Products.Queries.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing products
/// </summary>
[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all products with pagination
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paged list of products</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllProductsQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paged list of products in the category</returns>
    [HttpGet("category/{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductsByCategory(
        Guid categoryId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetProductsByCategoryQuery(categoryId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="command">Product creation details</param>
    /// <returns>Created product ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = result.Value },
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="command">Product update details</param>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.ProductId)
            return BadRequest(new { error = "Product ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "Product updated successfully" });
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
