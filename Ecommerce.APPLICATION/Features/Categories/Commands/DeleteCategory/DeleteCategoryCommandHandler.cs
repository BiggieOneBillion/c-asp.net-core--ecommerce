using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(
        DeleteCategoryCommand request,
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

            await _categoryRepository.DeleteAsync(category);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("Category.DeleteFailed", $"Failed to delete category: {ex.Message}"));
        }
    }
}
