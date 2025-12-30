using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categoryId = Guid.NewGuid();

            var category = new Category(
                request.CategoryName,
                request.CategoryDescription,
                categoryId,
                request.ActiveStatus);

            await _categoryRepository.CreateAsync(category);

            return Result.Success(categoryId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Category.CreateFailed", $"Failed to create category: {ex.Message}"));
        }
    }
}
