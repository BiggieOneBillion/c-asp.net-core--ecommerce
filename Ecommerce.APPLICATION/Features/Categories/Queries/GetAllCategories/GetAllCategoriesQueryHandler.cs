using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler 
    : IRequestHandler<GetAllCategoriesQuery, Result<GeneralResponse<List<CategoryResponseDTO>>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<List<CategoryResponseDTO>>>> Handle(
        GetAllCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryResponseDTO>>(categories);

            return Result<GeneralResponse<List<CategoryResponseDTO>>>.Success(
                GeneralResponse<List<CategoryResponseDTO>>.CreateSuccess(categoryDtos));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<List<CategoryResponseDTO>>>(
                new Error("Category.QueryFailed", $"Failed to retrieve categories: {ex.Message}"));
        }
    }
}
