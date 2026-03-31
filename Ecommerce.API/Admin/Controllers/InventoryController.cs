using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Inventory;
using Ecommerce.APPLICATION.Features.Inventory.Admin.Commands.CreateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Admin.Commands.UpdateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Admin.Queries.GetInventoryByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/inventory")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("product/{productId:guid}")]
    [SwaggerOperation(Summary = "Get inventory by product ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInventoryByProduct(Guid productId)
    {
        var result = await _mediator.Send(new GetInventoryByProductQuery(productId));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create inventory for a product")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDTO dto)
    {
        var command = new CreateInventoryCommand(dto.ProductId, dto.StockQuantity, dto.ReservedQuantity);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update inventory record")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateInventory(Guid id, [FromBody] UpdateInventoryDTO dto)
    {
        var command = new UpdateInventoryCommand(id, dto.ProductId, dto.StockQuantity, dto.ReservedQuantity);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
