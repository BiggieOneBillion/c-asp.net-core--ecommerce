using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = CategoryId.Create(request.CategoryId);
            var category = await _categoryRepository.GetByIdAsync(categoryId.Id);

            if (category == null)
            {
                return Result.Failure(
                    new Error("Category.NotFound", $"Category with ID {request.CategoryId} not found"));
            }

            category.UpdateDetails(request.CategoryName, request.CategoryDescription, request.ActiveStatus);

            await _categoryRepository.UpdateAsync(category);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("Category.UpdateFailed", $"Failed to update category: {ex.Message}"));
        }
    }
}
