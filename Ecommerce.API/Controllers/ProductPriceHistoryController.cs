using Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.CreateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Queries.GetPriceHistoryByProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing product price history
/// </summary>
[ApiController]
[Route("api/v1/pricehistory")]
[Produces("application/json")]
public class ProductPriceHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductPriceHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get price history by product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paged list of price history for the product</returns>
    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPriceHistoryByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetPriceHistoryByProductQuery(productId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new price history record
    /// </summary>
    /// <param name="command">Price history creation details</param>
    /// <returns>Created price history ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductPriceHistory([FromBody] CreateProductPriceHistoryCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Created(
            string.Empty,
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing price history record
    /// </summary>
    /// <param name="id">Price history ID</param>
    /// <param name="command">Price history update details</param>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductPriceHistory(Guid id, [FromBody] UpdateProductPriceHistoryCommand command)
    {
        if (id != command.ProductPriceHistoryId)
            return BadRequest(new { error = "Price history ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "Price history updated successfully" });
    }
}
