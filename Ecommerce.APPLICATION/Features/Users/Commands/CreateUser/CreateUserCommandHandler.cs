using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Services;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check if user with email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result.Failure<Guid>(
                    new Error("User.EmailExists", "A user with this email already exists"));
            }

            var userId = Guid.NewGuid();
            var hashedPassword = _passwordService.HashPassword(request.Password);

            var user = new Users(
                request.Name,
                request.Email,
                hashedPassword,
                userId);

            await _userRepository.CreateAsync(user);

            return Result.Success(userId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("User.CreateFailed", $"Failed to create user: {ex.Message}"));
        }
    }
}
