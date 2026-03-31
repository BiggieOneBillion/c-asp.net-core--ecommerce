using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Admin.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler 
    : IRequestHandler<UpdateCategoryCommand, Result<GeneralResponse<Unit>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Category.NotFound", $"Category with ID {request.CategoryId} not found"));

            category.UpdateDetails(request.Name, request.Description, category.ActiveStatus);

            await _categoryRepository.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Category updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Category.UpdateFailed", $"Failed to update category: {ex.Message}"));
        }
    }
}
