using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDTO>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
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

            return Result.Success(productDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<ProductResponseDTO>(
                new Error("Product.QueryFailed", $"Failed to retrieve product: {ex.Message}"));
        }
    }
}
