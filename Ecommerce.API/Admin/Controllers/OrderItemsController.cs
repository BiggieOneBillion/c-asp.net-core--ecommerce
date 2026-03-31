using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.OrderItems;
using Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.CreateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.DeleteOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.UpdateOrderItem;
using Ecommerce.APPLICATION.Features.OrderItems.Admin.Queries.GetOrderItemById;
using Ecommerce.APPLICATION.Features.OrderItems.Admin.Queries.GetOrderItemsByOrder;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/orderitems")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get order item by ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderItemById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderItemByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet("order/{orderId:guid}")]
    [SwaggerOperation(Summary = "Get order items by order ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderItemsByOrder(
        Guid orderId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrderItemsByOrderQuery(orderId, pageNumber, pageSize));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new order item")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDTO dto)
    {
        var command = new CreateOrderItemCommand(dto.OrderId, dto.ProductId, dto.Quantity, dto.CreateAt);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update an order item")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] UpdateOrderItemDTO dto)
    {
        var command = new UpdateOrderItemCommand(id, dto.OrderId, dto.ProductId, dto.Quantity, dto.CreateAt);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete an order item")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderItemCommand(id));
        return result.ProcessResult(this);
    }
}
