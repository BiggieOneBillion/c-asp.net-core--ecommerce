using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Order;
using Ecommerce.APPLICATION.Features.Orders.Public.Commands.CreateOrder;
using Ecommerce.APPLICATION.Features.Orders.Public.Queries.GetOrderById;
using Ecommerce.APPLICATION.Features.Orders.Public.Queries.GetOrdersByUser;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Public.Controllers;

[ApiController]
[Route("api/v1/orders")]
[Produces("application/json")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public OrdersController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get order by ID")]
    [ProducesResponseType(typeof(GeneralResponse<Ecommerce.APPLICATION.ResponseDTOs.OrderResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet("user/{userId:guid}")]
    [SwaggerOperation(Summary = "Get orders by user ID")]
    [ProducesResponseType(typeof(GeneralResponse<List<Ecommerce.APPLICATION.ResponseDTOs.OrderResponseDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        // Only the owner or an Admin can view orders for a specific user
        var currentUserId = _currentUserService.UserId;
        if (userId.ToString() != currentUserId && !User.IsInRole("Admin"))
        {
            var forbidden = Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<object>.CreateFailure(
                "You are not authorized to view orders for this user.", 403);
            return new ObjectResult(forbidden) { StatusCode = 403 };
        }

        var result = await _mediator.Send(new GetOrdersByUserQuery(userId));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new order")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
    {
        var command = new CreateOrderCommand(
            dto.UserId,
            dto.Items.Select(i => new OrderItemDTO(i.ProductId, i.Quantity)).ToList(),
            dto.CouponCode
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
