using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Category;
using Ecommerce.APPLICATION.Features.Categories.Admin.Commands.CreateCategory;
using Ecommerce.APPLICATION.Features.Categories.Admin.Commands.DeleteCategory;
using Ecommerce.APPLICATION.Features.Categories.Admin.Commands.UpdateCategory;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/categories")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class AdminCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new category")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
    {
        Guid? parentId = string.IsNullOrEmpty(dto.ParentCategoryId)
            ? null
            : Guid.Parse(dto.ParentCategoryId);

        var command = new CreateCategoryCommand(dto.Name, dto.Description, parentId);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update an existing category")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDTO dto)
    {
        var command = new UpdateCategoryCommand(id, dto.Name, dto.Description, dto.ActiveStatus, null);
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a category")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        return result.ProcessResult(this);
    }
}
