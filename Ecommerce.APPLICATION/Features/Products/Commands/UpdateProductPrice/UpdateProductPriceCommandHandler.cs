using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProductPrice;

public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return Result.Failure<Guid>(new Error("Product.NotFound", "Product not found"));
            }

            product.UpdatePrice(request.NewPrice);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id.Id);
        }
        catch (DomainException ex)
        {
            return Result.Failure<Guid>(new Error("Product.DomainError", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(new Error("Product.UpdatePriceFailed", $"Failed to update product price: {ex.Message}"));
        }
    }
}
