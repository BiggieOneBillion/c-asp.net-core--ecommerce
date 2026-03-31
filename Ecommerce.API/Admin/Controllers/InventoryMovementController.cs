using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.InventoryMovement;
using Ecommerce.APPLICATION.Features.InventoryMovement.Admin.Commands.CreateInventoryMovement;
using Ecommerce.APPLICATION.Features.InventoryMovement.Admin.Queries.GetInventoryMovementsByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/inventorymovement")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class InventoryMovementController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryMovementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("product/{productId:guid}")]
    [SwaggerOperation(Summary = "Get inventory movements for a product")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInventoryMovementsByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetInventoryMovementsByProductQuery(productId, pageNumber, pageSize));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create an inventory movement record")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateInventoryMovement([FromBody] CreateInventoryMovementDTO dto)
    {
        var command = new CreateInventoryMovementCommand(dto.ProductId, dto.QuantityChanged, dto.MovementType, dto.Reason);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
