using Ecommerce.API.Extensions;
using Ecommerce.APPLICATION.DTOs.Product;
using Ecommerce.APPLICATION.Features.Products.Admin.Commands.CreateProduct;
using Ecommerce.APPLICATION.Features.Products.Admin.Commands.DeleteProduct;
using Ecommerce.APPLICATION.Features.Products.Admin.Commands.UpdateProduct;
using Ecommerce.APPLICATION.Features.Products.Admin.Commands.UpdateProductPrice;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.API.Admin.Controllers;

[ApiController]
[Route("api/v1/admin/products")]
[Produces("application/json")]
[Authorize(Policy = "AdminOnly")]
public class AdminProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new product")]
    [ProducesResponseType(typeof(GeneralResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
    {
        var command = new CreateProductCommand(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.StockQuantity,
            dto.CategoryId,
            dto.ImageUrl
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update an existing product")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO dto)
    {
        var command = new UpdateProductCommand(
            id,
            dto.Name,
            dto.Description,
            dto.CategoryId,
            dto.ImageUrl,
            dto.IsActive
        );
        var result = await _mediator.Send(command);
        return result.ProcessResult(this);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a product")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return result.ProcessResult(this);
    }

    [HttpPatch("{id:guid}/price")]
    [SwaggerOperation(Summary = "Update product price")]
    [ProducesResponseType(typeof(GeneralResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GeneralResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdateProductPriceDTO dto)
    {
        var result = await _mediator.Send(new UpdateProductPriceCommand(id, dto.NewPrice));
        return result.ProcessResult(this);
    }
}
