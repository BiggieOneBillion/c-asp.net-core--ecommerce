using Ecommerce.APPLICATION.DTOs.Category;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Category
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Category description is required.")
                .MaximumLength(500)
                .WithMessage("Category description must not exceed 500 characters.");
            
            RuleFor(x => x.ParentCategoryId)
                .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
                .WithMessage("Parent category ID must be a valid GUID.");
        }
    }
}
