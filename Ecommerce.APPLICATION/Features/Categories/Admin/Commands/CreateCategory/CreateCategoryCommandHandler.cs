using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Admin.Commands.CreateCategory;

public class CreateCategoryCommandHandler 
    : IRequestHandler<CreateCategoryCommand, Result<GeneralResponse<Guid>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            // Note: ParentCategoryId is not supported by the current Category entity
            var category = Category.Create(
                request.Name,
                request.Description,
                activeStatus: true
            );

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(category.Id.Id, "Category created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("Category.CreateFailed", $"Failed to create category: {ex.Message}"));
        }
    }
}
