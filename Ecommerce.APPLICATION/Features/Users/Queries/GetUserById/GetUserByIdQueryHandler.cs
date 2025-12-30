using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Users;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<CreateUserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateUserDTO>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return Result.Failure<CreateUserDTO>(
                    new Error("User.NotFound", $"User with ID {request.UserId} not found"));
            }

            var userDto = _mapper.Map<CreateUserDTO>(user);

            return Result.Success(userDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreateUserDTO>(
                new Error("User.QueryFailed", $"Failed to retrieve user: {ex.Message}"));
        }
    }
}
