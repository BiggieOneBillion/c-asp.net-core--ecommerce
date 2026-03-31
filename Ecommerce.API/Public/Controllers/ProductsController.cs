using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.Features.Products.Public.Queries.GetAllProducts;
using Ecommerce.APPLICATION.Features.Products.Public.Queries.GetProductById;
using Ecommerce.APPLICATION.Features.Products.Public.Queries.GetProductsByCategory;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.APPLICATION.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Public.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
[AllowAnonymous]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all products with pagination")]
    [ProducesResponseType(typeof(GeneralResponse<PagedResult<ProductResponseDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(pageNumber, pageSize));
        return result.ProcessResult(this);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get product by ID")]
    [ProducesResponseType(typeof(GeneralResponse<ProductResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result.ProcessResult(this);
    }

    [HttpGet("category/{categoryId:guid}")]
    [SwaggerOperation(Summary = "Get products by category ID with pagination")]
    [ProducesResponseType(typeof(GeneralResponse<PagedResult<ProductResponseDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductsByCategory(Guid categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryId, pageNumber, pageSize));
        return result.ProcessResult(this);
    }
}
