using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository, 
        IInventoryRepository inventoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var product = Product.Create(
                request.Name,
                request.Description,
                CategoryId.Create(request.CategoryId),
                request.Price
            );

            await _productRepository.CreateAsync(product);

            // Use fully qualified name to avoid collision with Inventory feature namespace
            var inventory = Ecommerce.CORE.Entity.Inventory.Create(product.Id, request.StockQuantity);
            await _inventoryRepository.CreateAsync(inventory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(product.Id.Id, "Product created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("Product.CreateFailed", $"Failed to create product: {ex.Message}"));
        }
    }
}
