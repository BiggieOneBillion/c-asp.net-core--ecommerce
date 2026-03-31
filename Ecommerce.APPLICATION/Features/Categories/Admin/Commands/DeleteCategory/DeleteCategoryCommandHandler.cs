using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Admin.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler 
    : IRequestHandler<DeleteCategoryCommand, Result<GeneralResponse<Unit>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        DeleteCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category == null)
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Category.NotFound", $"Category with ID {request.Id} not found"));

            await _categoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Category deleted successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Category.DeleteFailed", $"Failed to delete category: {ex.Message}"));
        }
    }
}
