using Ecommerce.APPLICATION.Features.Discounts.Commands.CreateDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Queries.GetActiveDiscounts;
using Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountAnalytics;
using Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountById;
using Ecommerce.APPLICATION.Features.Discounts.Queries.ValidateCoupon;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/v1/discounts")]
[Produces("application/json")]
public class DiscountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
        var result = await _mediator.Send(new GetActiveDiscountsQuery());
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetDiscountByIdQuery(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpGet("analytics")]
    public async Task<IActionResult> GetAnalytics()
    {
        var result = await _mediator.Send(new GetDiscountAnalyticsQuery());
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCoupon([FromBody] ValidateCouponQuery query)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDiscount(Guid id, [FromBody] UpdateDiscountCommand command)
    {
        if (id != command.Id) return BadRequest(GeneralResponse<object>.CreateFailure("ID mismatch", 400));
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDiscount(Guid id)
    {
        var result = await _mediator.Send(new DeleteDiscountCommand(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }
}
