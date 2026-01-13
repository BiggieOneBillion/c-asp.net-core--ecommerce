using FluentValidation;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Features.Auth.Commands.Register;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.]").WithMessage("Password must contain at least one special character (!?*.).");

        RuleFor(x => x.Request.ConfirmPassword)
            .Equal(x => x.Request.Password).WithMessage("Passwords do not match.");
    }
}
