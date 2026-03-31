using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.Features.Categories.Public.Queries.GetAllCategories;
using Ecommerce.APPLICATION.Features.Categories.Public.Queries.GetCategoryById;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Public.Controllers;

[ApiController]
[Route("api/v1/categories")]
[Produces("application/json")]
[AllowAnonymous]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get category by ID")]
    [ProducesResponseType(typeof(GeneralResponse<CategoryResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all categories")]
    [ProducesResponseType(typeof(GeneralResponse<List<CategoryResponseDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery());
        return result.ProcessResult(this);
    }
}
