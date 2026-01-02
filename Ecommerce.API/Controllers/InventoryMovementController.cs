using Ecommerce.APPLICATION.Features.InventoryMovement.Commands.CreateInventoryMovement;
using Ecommerce.APPLICATION.Features.InventoryMovement.Queries.GetInventoryMovementsByProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing inventory movements
/// </summary>
[ApiController]
[Route("api/v1/inventorymovement")]
[Produces("application/json")]
public class InventoryMovementController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryMovementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get inventory movements by product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paged list of inventory movements for the product</returns>
    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetInventoryMovementsByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetInventoryMovementsByProductQuery(productId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new inventory movement record
    /// </summary>
    /// <param name="command">Inventory movement creation details</param>
    /// <returns>Created inventory movement ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInventoryMovement([FromBody] CreateInventoryMovementCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Created(
            string.Empty,
            new { id = result.Value });
    }
}
