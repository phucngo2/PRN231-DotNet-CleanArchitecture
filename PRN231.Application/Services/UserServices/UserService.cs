using AutoMapper;
using LanguageExt.Common;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Application.Services.UserServices.Dtos;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Exceptions.User;
using PRN231.Domain.Interfaces.UnitOfWork;

namespace PRN231.Application.Services.UserServices;

public class UserService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserIdentityService userIdentityService
) : IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdentityService _userIdentityService = userIdentityService;

    public async Task<Result<UserResponseDto>> GetCurrentUserAsync()
    {
        var userId = _userIdentityService.GetUserId();
        if (userId is null)
        {
            return new Result<UserResponseDto>(new UserUnauthorizedException());
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return new Result<UserResponseDto>(new UserNotFoundException());
        }

        return _mapper.Map<UserResponseDto>(user);
    }
}
