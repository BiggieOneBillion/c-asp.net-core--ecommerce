using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Category;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CreateCategoryDTO>>
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

    public async Task<Result<CreateCategoryDTO>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = CategoryId.Create(request.CategoryId);
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return Result.Failure<CreateCategoryDTO>(
                    new Error("Category.NotFound", $"Category with ID {request.CategoryId} not found"));
            }

            var categoryDto = _mapper.Map<CreateCategoryDTO>(category);

            return Result.Success(categoryDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreateCategoryDTO>(
                new Error("Category.QueryFailed", $"Failed to retrieve category: {ex.Message}"));
        }
    }
}
