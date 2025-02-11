using LanguageExt.Common;
using PRN231.Application.Services.UserServices.Dtos;

namespace PRN231.Application.Services.UserServices;

public interface IUserService
{
    Task<Result<UserResponseDto>> GetCurrentUserAsync();
}
