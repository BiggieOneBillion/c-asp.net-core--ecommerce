using Ecommerce.APPLICATION.Features.Users.Commands.CreateUser;
using Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;
using Ecommerce.APPLICATION.Features.Users.Commands.UpdateUser;
using Ecommerce.APPLICATION.Features.Users.Queries.GetUserByEmail;
using Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

/// <summary>
/// Controller for managing users
/// </summary>
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

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">User email address</param>
    /// <returns>User details</returns>
    [HttpGet("email/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var query = new GetUserByEmailQuery(email);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="command">User creation details</param>
    /// <returns>Created user ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Message });

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = result.Value },
            new { id = result.Value });
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="command">User update details</param>
    /// <returns>Success message</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.UserId)
            return BadRequest(new { error = "User ID mismatch" });

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return Ok(new { message = "User updated successfully" });
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return NotFound(new { error = result.Error.Message });

        return NoContent();
    }
}
