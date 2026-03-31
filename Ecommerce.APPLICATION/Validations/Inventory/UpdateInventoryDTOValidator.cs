using Ecommerce.APPLICATION.DTOs.Inventory;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Inventory
{
    public class UpdateInventoryDTOValidator : AbstractValidator<UpdateInventoryDTO>
    {
        public UpdateInventoryDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity must be greater than or equal to 0.");

            RuleFor(x => x.ReservedQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Reserved quantity must be greater than or equal to 0.");

            RuleFor(x => x)
                .Must(x => x.ReservedQuantity <= x.StockQuantity)
                .WithMessage("Reserved quantity cannot exceed stock quantity.");
        }
    }
}
