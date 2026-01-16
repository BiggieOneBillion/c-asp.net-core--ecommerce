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
    /// Retrieves all currently active and valid discounts.
    /// </summary>
    /// <remarks>
    /// Returns a list of discounts where the current date is within the StartDate and EndDate range, and IsActive is true.
    /// Requires **Discounts.View** permission.
    /// </remarks>
    /// <returns>A list of active discount DTOs</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<DiscountResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetActive()
    {
        var result = await _mediator.Send(new GetActiveDiscountsQuery());
        return Ok(result.Value);
    }

    /// <summary>
    /// Retrieves a specific discount by its unique identifier.
    /// </summary>
    /// <param name="id">The GUID identifier of the discount</param>
    /// <remarks>
    /// Requires **Discounts.View** permission.
    /// </remarks>
    /// <returns>The requested discount details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DiscountResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetDiscountByIdQuery(id));

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Retrieves aggregated analytics and performance metrics for all discounts.
    /// </summary>
    /// <remarks>
    /// Accessible only to administrators or staff with management access.
    /// Requires **Discounts.Manage** permission.
    /// </remarks>
    /// <returns>A summary of system-wide discount performance</returns>
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
    /// Validates a coupon code against a specific order total.
    /// </summary>
    /// <param name="query">Validation request containing the code and order total</param>
    /// <remarks>
    /// This endpoint checks for expiration, usage limits, and minimum amount requirements.
    /// It does NOT require a specific permission as it is typically used by customers during checkout.
    /// </remarks>
    /// <returns>Information about whether the coupon is valid and the potential discount amount</returns>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(CouponValidationResultDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateCoupon([FromBody] ValidateCouponQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Creates a new discount or coupon.
    /// </summary>
    /// <param name="command">The discount definition</param>
    /// <remarks>
    /// Used by administrators to set up new promotions, automatic discounts, or coupon codes.
    /// Requires **Discounts.Create** permission.
    /// </remarks>
    /// <returns>The ID of the newly created discount</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
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
    /// Updates basic information for an existing discount.
    /// </summary>
    /// <param name="id">The identifier of the discount to update</param>
    /// <param name="command">The updated data (Name, Description, and IsActive status)</param>
    /// <remarks>
    /// Note: Type, Value, and Scope cannot be changed after creation to maintain analytical integrity.
    /// Requires **Discounts.Update** permission.
    /// </remarks>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
    /// Permanently removes a discount from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the discount to delete</param>
    /// <remarks>
    /// Caution: Deleting a discount might affect historical data analysis. Consider deactivating instead.
    /// Requires **Discounts.Delete** permission.
    /// </remarks>
    /// <returns>No content on success</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDiscount(Guid id)
    {
        var result = await _mediator.Send(new DeleteDiscountCommand(id));

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
