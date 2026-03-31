using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Admin.Commands.UpdateProductPrice;

public class UpdateProductPriceCommandHandler 
    : IRequestHandler<UpdateProductPriceCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateProductPriceCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));

            product.UpdatePrice(request.NewPrice);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Product price updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Product.PriceUpdateFailed", $"Failed to update product price: {ex.Message}"));
        }
    }
}
