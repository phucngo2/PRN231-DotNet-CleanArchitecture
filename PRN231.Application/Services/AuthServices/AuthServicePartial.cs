using PRN231.Application.Helpers;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Exceptions.User;

namespace PRN231.Application.Services.AuthServices;

public partial class AuthService
{
    private static void VerifyLogin(User user, string password)
    {
        var passwordVerified = HashHelpers.VerifyPassword(password, user.Password);
        if (!passwordVerified)
        {
            throw new WrongCredentialsException();
        }
    }

    private static void VerifyConfirmPassword(string password, string passwordConfirm)
    {
        if (password != passwordConfirm)
        {
            throw new PasswordMustMatchException();
        }
    }

    private async Task<User> GetUserByIdentity()
    {
        var userId = _userIdentityService.GetUserId()
            ?? throw new UserUnauthorizedException();

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId)
            ?? throw new UserNotFoundException();

        return user;
    }
}
