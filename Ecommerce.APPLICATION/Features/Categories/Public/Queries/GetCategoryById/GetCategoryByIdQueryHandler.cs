using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Public.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler 
    : IRequestHandler<GetCategoryByIdQuery, Result<GeneralResponse<CategoryResponseDTO>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<CategoryResponseDTO>>> Handle(
        GetCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category == null)
                return Result.Failure<GeneralResponse<CategoryResponseDTO>>(
                    new Error("Category.NotFound", $"Category with ID {request.Id} not found"));

            var categoryDto = _mapper.Map<CategoryResponseDTO>(category);

            return Result<GeneralResponse<CategoryResponseDTO>>.Success(
                GeneralResponse<CategoryResponseDTO>.CreateSuccess(categoryDto));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<CategoryResponseDTO>>(
                new Error("Category.QueryFailed", $"Failed to retrieve category: {ex.Message}"));
        }
    }
}
