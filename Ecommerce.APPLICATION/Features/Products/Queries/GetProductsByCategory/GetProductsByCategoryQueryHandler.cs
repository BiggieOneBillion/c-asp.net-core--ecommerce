using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Product;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler 
    : IRequestHandler<GetProductsByCategoryQuery, Result<PagedResult<CreateProductDTO>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<CreateProductDTO>>> Handle(
        GetProductsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = CategoryId.Create(request.CategoryId);
            var products = await _productRepository.GetByCategoryAsync(categoryId);

            // Calculate pagination
            var totalCount = products.Count;
            var items = products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var productDtos = _mapper.Map<List<CreateProductDTO>>(items);

            var pagedResult = new PagedResult<CreateProductDTO>(
                productDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<CreateProductDTO>>(
                new Error("Product.QueryFailed", $"Failed to retrieve products by category: {ex.Message}"));
        }
    }
}
