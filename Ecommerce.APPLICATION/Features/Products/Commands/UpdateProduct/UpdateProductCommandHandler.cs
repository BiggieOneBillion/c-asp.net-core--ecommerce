using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler 
    : IRequestHandler<UpdateProductCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateProductCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));

            product.Name = request.Name;
            product.Description = request.Description;
            product.CategoryId = CategoryId.Create(request.CategoryId);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Product updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Product.UpdateFailed", $"Failed to update product: {ex.Message}"));
        }
    }
}
