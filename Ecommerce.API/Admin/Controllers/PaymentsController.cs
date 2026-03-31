using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Payment;
using Ecommerce.APPLICATION.Features.Payments.Admin.Commands.CreatePayment;
using Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentById;
using Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentsByOrder;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/payments")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get payment by ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentById(Guid id)
    {
        var result = await _mediator.Send(new GetPaymentByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet("order/{orderId:guid}")]
    [SwaggerOperation(Summary = "Get payments by order ID")]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentsByOrder(
        Guid orderId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPaymentsByOrderQuery(orderId, pageNumber, pageSize));
        return result.ProcessResult(this);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new payment")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO dto)
    {
        var command = new CreatePaymentCommand(
            dto.PaymentType,
            dto.Amount,
            dto.PaymentDate,
            dto.OrderId
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
