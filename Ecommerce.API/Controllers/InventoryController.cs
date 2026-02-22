using Ecommerce.APPLICATION.Features.Inventory.Commands.CreateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Commands.UpdateInventory;
using Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

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

    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetInventoryByProduct(Guid productId)
    {
        var result = await _mediator.Send(new GetInventoryByProductQuery(productId));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInventory(Guid id, [FromBody] UpdateInventoryCommand command)
    {
        // Keep original behavior as much as possible but use GeneralResponse for errors
        return BadRequest(GeneralResponse<object>.CreateFailure("Endpoint disabled", 400));
    }
}
