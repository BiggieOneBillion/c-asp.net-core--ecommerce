using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler 
    : IRequestHandler<GetAllProductsQuery, Result<GeneralResponse<PagedResult<ProductResponseDTO>>>>
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

    public async Task<Result<GeneralResponse<PagedResult<ProductResponseDTO>>>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var products = (await _productRepository.GetAllAsync()).ToList();

            if (products == null || !products.Any())
                return Result<GeneralResponse<PagedResult<ProductResponseDTO>>>.Success(
                    GeneralResponse<PagedResult<ProductResponseDTO>>.CreateSuccess(
                        new PagedResult<ProductResponseDTO>(new List<ProductResponseDTO>(), request.PageNumber, request.PageSize, 0),
                        "No products found"));

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
                    (product.Id.Id, product.CategoryId.Id, product.CurrentPrice, 1)
                };

                var discountResult = await _discountService.CalculateDiscountAsync(null, product.CurrentPrice, discountItems);
                var discountAmount = discountResult.DiscountAmount;

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

            return Result<GeneralResponse<PagedResult<ProductResponseDTO>>>.Success(
                GeneralResponse<PagedResult<ProductResponseDTO>>.CreateSuccess(pagedResult));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<PagedResult<ProductResponseDTO>>>(
                new Error("Product.QueryFailed", $"Failed to retrieve products: {ex.Message}"));
        }
    }
}
