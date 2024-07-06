using Microsoft.AspNetCore.Identity;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Exceptions.User;

namespace PRN231.Application.Services.AuthServices;

public partial class AuthService
{
    private void VerifyLogin(User user, string password)
    {
        var passwordVerified = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (passwordVerified == PasswordVerificationResult.Failed)
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

    private async Task<User> GetUserByEmail(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email)
            ?? throw new UserNotFoundException();

        return user;
    }

    private async Task<UserToken> GetValidUserToken(string token)
    {
        var userToken = await _unitOfWork.UserTokenRepository.GetNotExpiredByTokenAsync(token)
            ?? throw new InvalidTokenException();

        return userToken;
    }

    private void UpdateUserPassword(User user, string password)
    {
        user.Password = _passwordHasher.HashPassword(user, password);
        _unitOfWork.UserRepository.Update(user);
    }
}
