using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return Result.Failure(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));
            }

            await _productRepository.DeleteAsync(productId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("Product.DeleteFailed", $"Failed to delete product: {ex.Message}"));
        }
    }
}
