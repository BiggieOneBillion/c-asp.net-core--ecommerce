using Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;
using Ecommerce.APPLICATION.Features.Products.Commands.DeleteProduct;
using Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;
using Ecommerce.APPLICATION.Features.Products.Commands.UpdateProductPrice;
using Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;
using Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;
using Ecommerce.APPLICATION.Features.Products.Queries.GetProductsByCategory;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/v1/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(pageNumber, pageSize));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpGet("category/{categoryId:guid}")]
    public async Task<IActionResult> GetProductsByCategory(Guid categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryId, pageNumber, pageSize));
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return StatusCode(201, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.ProductId) return BadRequest(GeneralResponse<object>.CreateFailure("Product ID mismatch", 400));
        var result = await _mediator.Send(command);
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        if (!result.IsSuccess) return NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404));
        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/price")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdateProductPriceRequest request)
    {
        var result = await _mediator.Send(new UpdateProductPriceCommand(id, request.NewPrice));
        if (!result.IsSuccess) return result.Error.Code == "Product.NotFound" 
            ? NotFound(GeneralResponse<object>.CreateFailure(result.Error.Message, 404))
            : BadRequest(GeneralResponse<object>.CreateFailure(result.Error.Message, 400));
        return Ok(result.Value);
    }
}

public record UpdateProductPriceRequest(decimal NewPrice);
