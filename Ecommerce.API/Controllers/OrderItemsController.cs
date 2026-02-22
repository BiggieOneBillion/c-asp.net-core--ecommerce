using Ecommerce.APPLICATION.Features.OrderItems.Commands.CreateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Commands.UpdateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemById;
using Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemsByOrder;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderItemById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderItemByIdQuery(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetOrderItemsByOrder(
        Guid orderId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrderItemsByOrderQuery(orderId, pageNumber, pageSize));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] UpdateOrderItemCommand command)
    {
        if (id != command.OrderItemId) return BadRequest(GeneralResponse<object>.CreateFailure("Order item ID mismatch", 400));
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderItemCommand(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }
}
