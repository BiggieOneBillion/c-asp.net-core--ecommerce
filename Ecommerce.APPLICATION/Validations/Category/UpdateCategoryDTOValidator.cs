using Ecommerce.APPLICATION.DTOs.Category;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Category
{
    public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.CategoryDescription)
                .NotEmpty()
                .WithMessage("Category description is required.")
                .MaximumLength(500)
                .WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
