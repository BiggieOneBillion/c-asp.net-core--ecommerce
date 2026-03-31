using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Users;
using Ecommerce.APPLICATION.Features.Users.Admin.Commands.CreateUser;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/users")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class AdminUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminUsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new admin user")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
    {
        var command = new CreateUserCommand(dto.Name, dto.Email, dto.Password);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }
}
