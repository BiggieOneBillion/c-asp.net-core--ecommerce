using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var product = await _productRepository.GetByIdAsync(productId.Id);

            if (product == null)
            {
                return Result.Failure(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));
            }

            var categoryId = CategoryId.Create(request.CategoryId);

            product.Name = request.Name;
            product.Description = request.Description;
            product.CategoryId = categoryId;
            product.CurrentPrice = request.CurrentPrice;

            await _productRepository.UpdateAsync(product);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("Product.UpdateFailed", $"Failed to update product: {ex.Message}"));
        }
    }
}
