using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = Guid.NewGuid();
            var categoryId = CategoryId.Create(request.CategoryId);

            var product = new Product(
                request.Name,
                request.Description,
                productId,
                categoryId,
                request.CurrentPrice);

            await _productRepository.CreateAsync(product);

            return Result.Success(productId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Product.CreateFailed", $"Failed to create product: {ex.Message}"));
        }
    }
}
