using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.APPLICATION.Services;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Admin.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<GeneralResponse<Guid>>>
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

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check if user with email already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Result.Failure<GeneralResponse<Guid>>(
                    new Error("User.EmailExists", "A user with this email already exists"));
            }

            var userId = Guid.NewGuid();
            var hashedPassword = _passwordService.HashPassword(request.Password);


            CORE.Entity.Users user = new(
                    name: request.Name,
                    email: request.Email,
                    passwordHash: hashedPassword,
                    userId: userId
                );

            await _userRepository.CreateAsync(user);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(userId, "User created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("User.CreateFailed", $"Failed to create user: {ex.Message}"));
        }
    }
}
