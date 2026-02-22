using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GeneralResponse<UserResponseDTO>>>
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

    public async Task<Result<GeneralResponse<UserResponseDTO>>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            
            var user = await _userRepository.GetByIdAsync(userId.Id);

            if (user == null)
            {
                return Result.Failure<GeneralResponse<UserResponseDTO>>(
                    new Error("User.NotFound", $"User with ID {request.UserId} not found"));
            }

            var userDto = _mapper.Map<UserResponseDTO>(user);

            return Result<GeneralResponse<UserResponseDTO>>.Success(
                GeneralResponse<UserResponseDTO>.CreateSuccess(userDto));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<UserResponseDTO>>(
                new Error("User.QueryFailed", $"Failed to retrieve user: {ex.Message}"));
        }
    }
}
