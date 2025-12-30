using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryResponseDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CategoryResponseDTO>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = CategoryId.Create(request.CategoryId);
            var category = await _categoryRepository.GetByIdAsync(categoryId.Id);

            if (category == null)
            {
                return Result.Failure<CategoryResponseDTO>(
                    new Error("Category.NotFound", $"Category with ID {request.CategoryId} not found"));
            }

            var categoryDto = _mapper.Map<CategoryResponseDTO>(category);

            return Result.Success(categoryDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CategoryResponseDTO>(
                new Error("Category.QueryFailed", $"Failed to retrieve category: {ex.Message}"));
        }
    }
}
