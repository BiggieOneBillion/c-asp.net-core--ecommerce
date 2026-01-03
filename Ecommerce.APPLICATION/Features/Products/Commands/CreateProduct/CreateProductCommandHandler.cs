using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = CategoryId.Create(request.CategoryId);

            var product = Product.Create(
                request.Name,
                request.Description,
                categoryId,
                request.CurrentPrice);

            await _productRepository.CreateAsync(product);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id.Id);
        }
        catch (DomainException ex)
        {
            return Result.Failure<Guid>(
                new Error("Product.DomainError", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Product.CreateFailed", $"Failed to create product: {ex.Message}"));
        }
    }
}
