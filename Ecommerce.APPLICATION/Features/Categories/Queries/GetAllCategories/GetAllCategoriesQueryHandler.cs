using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Category;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler 
    : IRequestHandler<GetAllCategoriesQuery, Result<PagedResult<CreateCategoryDTO>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<CreateCategoryDTO>>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();

            // Apply filtering
            if (request.ActiveOnly.HasValue)
            {
                categories = categories.Where(c => c.ActiveStatus == request.ActiveOnly.Value).ToList();
            }

            // Calculate pagination
            var totalCount = categories.Count;
            var items = categories
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var categoryDtos = _mapper.Map<List<CreateCategoryDTO>>(items);

            var pagedResult = new PagedResult<CreateCategoryDTO>(
                categoryDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<CreateCategoryDTO>>(
                new Error("Category.QueryFailed", $"Failed to retrieve categories: {ex.Message}"));
        }
    }
}
