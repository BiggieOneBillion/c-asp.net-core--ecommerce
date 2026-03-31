using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Users;
using Ecommerce.APPLICATION.Features.Users.Public.Commands.DeleteUser;
using Ecommerce.APPLICATION.Features.Users.Public.Commands.UpdateUser;
using Ecommerce.APPLICATION.Features.Users.Public.Queries.GetUserById;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Public.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UsersController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get user profile by ID")]
    [ProducesResponseType(typeof(GeneralResponse<UserResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Only the resource owner or an Admin can view a user profile
        var currentUserId = _currentUserService.UserId;
        if (id.ToString() != currentUserId && !User.IsInRole("Admin"))
        {
            var forbidden = Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<object>.CreateFailure(
                "You are not authorized to view this user's profile.", 403);
            return new ObjectResult(forbidden) { StatusCode = 403 };
        }

        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update user profile")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO dto)
    {
        // Only the resource owner or an Admin can update a user profile
        var currentUserId = _currentUserService.UserId;
        if (id.ToString() != currentUserId && !User.IsInRole("Admin"))
        {
            var forbidden = Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<object>.CreateFailure(
                "You are not authorized to update this user's profile.", 403);
            return new ObjectResult(forbidden) { StatusCode = 403 };
        }

        var command = new UpdateUserCommand(id, dto.Name, dto.Email, dto.Password);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete user account")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        // Only the resource owner or an Admin can delete a user account
        var currentUserId = _currentUserService.UserId;
        if (id.ToString() != currentUserId && !User.IsInRole("Admin"))
        {
            var forbidden = Ecommerce.APPLICATION.ResponseDTOs.GeneralResponse<object>.CreateFailure(
                "You are not authorized to delete this user account.", 403);
            return new ObjectResult(forbidden) { StatusCode = 403 };
        }

        var result = await _mediator.Send(new DeleteUserCommand(id));
        return result.ProcessResult(this);
    }
}
