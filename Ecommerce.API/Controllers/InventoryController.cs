using Ecommerce.APPLICATION.Features.Inventory.Commands.CreateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Commands.UpdateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing inventory
/// </summary>
[ApiController]
[Route("api/v1/inventory")]
[Produces("application/json")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get inventory by product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <returns>Inventory details for the product</returns>
    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInventoryByProduct(Guid productId)
    {
        var query = new GetInventoryByProductQuery(productId);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new inventory record
    /// </summary>
    /// <param name="command">Inventory creation details</param>
    /// <returns>Created inventory ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(
            nameof(GetInventoryByProduct),
            new { productId = command.ProductId },
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing inventory record
    /// </summary>
    /// <param name="id">Inventory ID</param>
    /// <param name="command">Inventory update details</param>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInventory(Guid id, [FromBody] UpdateInventoryCommand command)
    {
        await Task.CompletedTask;
        return BadRequest("Endpoint disabled");
    }
}
