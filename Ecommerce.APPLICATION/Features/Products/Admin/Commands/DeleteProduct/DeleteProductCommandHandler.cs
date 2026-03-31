using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Admin.Commands.DeleteProduct;

public class DeleteProductCommandHandler 
    : IRequestHandler<DeleteProductCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        DeleteProductCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Product.NotFound", $"Product with ID {request.Id} not found"));

            await _productRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Product deleted successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Product.DeleteFailed", $"Failed to delete product: {ex.Message}"));
        }
    }
}
