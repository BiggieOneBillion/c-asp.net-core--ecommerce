using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Discount;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Commands.CreateDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Commands.DeleteDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Commands.UpdateDiscount;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetActiveDiscounts;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetDiscountAnalytics;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetDiscountById;
using Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.ValidateCoupon;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/discounts")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class DiscountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all active discounts")]
    [ProducesResponseType(typeof(GeneralResponse<IEnumerable<object>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetActive()
    {
        var result = await _mediator.Send(new GetActiveDiscountsQuery());
        return result.ProcessResult(this);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get discount by ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetDiscountByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet("analytics")]
    [SwaggerOperation(Summary = "Get discount analytics")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnalytics()
    {
        var result = await _mediator.Send(new GetDiscountAnalyticsQuery());
        return result.ProcessResult(this);
    }

    [HttpPost("validate")]
    [SwaggerOperation(Summary = "Validate a coupon code")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ValidateCoupon([FromBody] ValidateCouponDTO dto)
    {
        var query = new ValidateCouponQuery(dto.CouponCode, dto.OrderTotal);
        var result = await _mediator.Send(query);
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new discount")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountDTO dto)
    {
        var command = new CreateDiscountCommand(
            dto.Name,
            dto.Description,
            dto.CouponCode,
            dto.Value,
            dto.Type,
            dto.Scope,
            dto.StartDate,
            dto.EndDate,
            dto.IsActive,
            dto.MinimumOrderAmount,
            dto.UsageLimit,
            dto.ApplicableCategoryIds,
            dto.ApplicableProductIds
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update a discount")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDiscount(Guid id, [FromBody] UpdateDiscountDTO dto)
    {
        var command = new UpdateDiscountCommand(id, dto.Name, dto.Description, dto.IsActive);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a discount")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDiscount(Guid id)
    {
        var result = await _mediator.Send(new DeleteDiscountCommand(id));
        return result.ProcessResult(this);
    }
}
