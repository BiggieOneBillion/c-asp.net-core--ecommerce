using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Users;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<CreateUserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateUserDTO>> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return Result.Failure<CreateUserDTO>(
                    new Error("User.NotFound", $"User with email {request.Email} not found"));
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
