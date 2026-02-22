using Ecommerce.APPLICATION.Features.InventoryMovement.Commands.CreateInventoryMovement;
using Ecommerce.APPLICATION.Features.InventoryMovement.Queries.GetInventoryMovementsByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

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

    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetInventoryMovementsByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetInventoryMovementsByProductQuery(productId, pageNumber, pageSize));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventoryMovement([FromBody] CreateInventoryMovementCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }
}
