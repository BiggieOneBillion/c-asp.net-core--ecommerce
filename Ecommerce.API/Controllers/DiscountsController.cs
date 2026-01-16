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

    /// <summary>
    /// Get all active discounts
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DiscountResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive()
    {
        var result = await _mediator.Send(new GetActiveDiscountsQuery());
        return Ok(result.Value);
    }

    /// <summary>
    /// Get discount by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DiscountResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetDiscountByIdQuery(id));

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get discount analytics and performance
    /// </summary>
    [HttpGet("analytics")]
    [ProducesResponseType(typeof(DiscountAnalyticsResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAnalytics()
    {
        var result = await _mediator.Send(new GetDiscountAnalyticsQuery());

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Validate a coupon code
    /// </summary>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(CouponValidationResultDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateCoupon([FromBody] ValidateCouponQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new discount
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(nameof(CreateDiscount), new { id = result.Value }, new { id = result.Value });
    }

    /// <summary>
    /// Update an existing discount
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDiscount(Guid id, [FromBody] UpdateDiscountCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { error = "ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "Discount updated successfully" });
    }

    /// <summary>
    /// Delete a discount
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDiscount(Guid id)
    {
        var result = await _mediator.Send(new DeleteDiscountCommand(id));

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
