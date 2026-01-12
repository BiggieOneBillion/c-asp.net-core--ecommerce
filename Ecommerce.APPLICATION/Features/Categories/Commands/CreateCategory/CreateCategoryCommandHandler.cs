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

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = Category.Create(
                request.CategoryName,
                request.CategoryDescription,
                request.ActiveStatus);

            await _categoryRepository.CreateAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Id.Id);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Category.CreateFailed", $"Failed to create category: {ex.Message}"));
        }
    }
}
