using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserResponseDTO>>
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

    public async Task<Result<UserResponseDTO>> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return Result.Failure<UserResponseDTO>(
                    new Error("User.NotFound", $"User with email {request.Email} not found"));
            }

            var userDto = _mapper.Map<UserResponseDTO>(user);

            return Result.Success(userDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<UserResponseDTO>(
                new Error("User.QueryFailed", $"Failed to retrieve user: {ex.Message}"));
        }
    }
}
