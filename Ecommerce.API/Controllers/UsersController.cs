using Ecommerce.APPLICATION.Features.Users.Commands.CreateUser;
using Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;
using Ecommerce.APPLICATION.Features.Users.Commands.UpdateUser;
using Ecommerce.APPLICATION.Features.Users.Queries.GetUserByEmail;
using Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var result = await _mediator.Send(new GetUserByEmailQuery(email));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.UserId) return BadRequest(GeneralResponse<object>.CreateFailure("User ID mismatch", 400));
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }
}
