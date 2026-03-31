using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.ProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Commands.CreateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Commands.UpdateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Queries.GetPriceHistoryByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/pricehistory")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class ProductPriceHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductPriceHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("product/{productId:guid}")]
    [SwaggerOperation(Summary = "Get price history for a product")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPriceHistoryByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPriceHistoryByProductQuery(productId, pageNumber, pageSize));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new price history record")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductPriceHistory([FromBody] CreateProductPriceHistoryDTO dto)
    {
        var command = new CreateProductPriceHistoryCommand(
            dto.ProductId,
            dto.NewPrice,
            dto.OldPrice,
            dto.EffectiveDate,
            dto.ChangedAt
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update a price history record")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductPriceHistory(Guid id, [FromBody] UpdateProductPriceHistoryDTO dto)
    {
        var command = new UpdateProductPriceHistoryCommand(
            id,
            dto.ProductId,
            dto.NewPrice,
            dto.OldPrice,
            dto.EffectiveDate,
            dto.ChangedAt
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
