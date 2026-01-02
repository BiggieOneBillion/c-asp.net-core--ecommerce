using Ecommerce.APPLICATION.Features.OrderItems.Commands.CreateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Commands.UpdateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemById;
using Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemsByOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing order items
/// </summary>
[ApiController]
[Route("api/v1/orderitems")]
[Produces("application/json")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get order item by ID
    /// </summary>
    /// <param name="id">Order item ID</param>
    /// <returns>Order item details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderItemById(Guid id)
    {
        var query = new GetOrderItemByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get order items by order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paged list of order items</returns>
    [HttpGet("order/{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrderItemsByOrder(
        Guid orderId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetOrderItemsByOrderQuery(orderId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new order item
    /// </summary>
    /// <param name="command">Order item creation details</param>
    /// <returns>Created order item ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(
            nameof(GetOrderItemById),
            new { id = result.Value },
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing order item
    /// </summary>
    /// <param name="id">Order item ID</param>
    /// <param name="command">Order item update details</param>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] UpdateOrderItemCommand command)
    {
        if (id != command.OrderItemId)
            return BadRequest(new { error = "Order item ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "Order item updated successfully" });
    }

    /// <summary>
    /// Delete an order item
    /// </summary>
    /// <param name="id">Order item ID</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        var command = new DeleteOrderItemCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
