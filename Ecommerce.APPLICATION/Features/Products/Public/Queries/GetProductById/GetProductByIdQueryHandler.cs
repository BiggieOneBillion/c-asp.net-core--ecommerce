using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Public.Queries.GetProductById;

public class GetProductByIdQueryHandler 
    : IRequestHandler<GetProductByIdQuery, Result<GeneralResponse<ProductResponseDTO>>>
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

    public async Task<Result<GeneralResponse<ProductResponseDTO>>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return Result.Failure<GeneralResponse<ProductResponseDTO>>(
                    new Error("Product.NotFound", $"Product with ID {request.Id} not found"));

            var productDto = _mapper.Map<ProductResponseDTO>(product);

            // Calculate auto-discounts
            var discountResult = await _discountService.CalculateDiscountAsync(
                null, 
                product.CurrentPrice, 
                new List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)> 
                { 
                    (product.Id.Id, product.CategoryId.Id, product.CurrentPrice, 1) 
                });
            
            var discountAmount = discountResult.DiscountAmount;

            if (discountAmount > 0)
            {
                productDto = productDto with 
                { 
                    DiscountedPrice = product.CurrentPrice - discountAmount,
                    DiscountPercentage = Math.Round((discountAmount / product.CurrentPrice) * 100, 2)
                };
            }

            return Result<GeneralResponse<ProductResponseDTO>>.Success(
                GeneralResponse<ProductResponseDTO>.CreateSuccess(productDto));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<ProductResponseDTO>>(
                new Error("Product.QueryFailed", $"Failed to retrieve product: {ex.Message}"));
        }
    }
}
