using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler 
    : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductResponseDTO>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IDiscountService discountService,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ProductResponseDTO>>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var products = (await _productRepository.GetAllAsync()).ToList();

            // Calculate pagination
            var totalCount = products.Count;
            var items = products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var productDtos = new List<ProductResponseDTO>();

            foreach (var product in items)
            {
                var productDto = _mapper.Map<ProductResponseDTO>(product);

                // Calculate auto-discounts
                var discountItems = new List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)>
                {
                    (Guid.Parse(product.Id.Value()), Guid.Parse(product.CategoryId.Value()), product.CurrentPrice, 1)
                };

                decimal discountAmount = await _discountService.CalculateDiscountAsync(null, product.CurrentPrice, discountItems);

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

                productDtos.Add(productDto);
            }

            var pagedResult = new PagedResult<ProductResponseDTO>(
                productDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<ProductResponseDTO>>(
                new Error("Product.QueryFailed", $"Failed to retrieve products: {ex.Message}"));
        }
    }
}
