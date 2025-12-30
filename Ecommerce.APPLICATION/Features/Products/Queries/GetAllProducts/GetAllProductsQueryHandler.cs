using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler 
    : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductResponseDTO>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
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

            var productDtos = _mapper.Map<List<ProductResponseDTO>>(items);

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
