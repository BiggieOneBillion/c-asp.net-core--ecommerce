using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDTO>>
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository,
        IDiscountService discountService,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    public async Task<Result<ProductResponseDTO>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var product = await _productRepository.GetByIdAsync(productId.Id);

            if (product == null)
            {
                return Result.Failure<ProductResponseDTO>(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));
            }

            var productDto = _mapper.Map<ProductResponseDTO>(product);

            // Calculate auto-discounts (no coupon code for listing)
            var discountItems = new List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)>
            {
                (Guid.Parse(product.Id.Value()), Guid.Parse(product.CategoryId.Value()), product.CurrentPrice, 1)
            };

            var (discountAmount, _) = await _discountService.CalculateDiscountAsync(null, product.CurrentPrice, discountItems);

            if (discountAmount > 0)
            {
                var discountedPrice = product.CurrentPrice - discountAmount;
                var discountPercentage = (discountAmount / product.CurrentPrice) * 100;
                
                productDto = productDto with 
                { 
                    DiscountedPrice = discountedPrice, 
                    DiscountPercentage = Math.Round(discountPercentage, 2) 
                };
            }

            return Result.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<ProductResponseDTO>(
                new Error("Product.QueryFailed", $"Failed to retrieve product: {ex.Message}"));
        }
    }
}
