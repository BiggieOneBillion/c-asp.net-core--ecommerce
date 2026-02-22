using Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.CreateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;
using Ecommerce.APPLICATION.Features.ProductPriceHistory.Queries.GetPriceHistoryByProduct;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/v1/pricehistory")]
[Produces("application/json")]
public class ProductPriceHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductPriceHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetPriceHistoryByProduct(
        Guid productId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPriceHistoryByProductQuery(productId, pageNumber, pageSize));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductPriceHistory([FromBody] CreateProductPriceHistoryCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProductPriceHistory(Guid id, [FromBody] UpdateProductPriceHistoryCommand command)
    {
        if (id != command.ProductPriceHistoryId) return BadRequest(GeneralResponse<object>.CreateFailure("Price history ID mismatch", 400));
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }
}
