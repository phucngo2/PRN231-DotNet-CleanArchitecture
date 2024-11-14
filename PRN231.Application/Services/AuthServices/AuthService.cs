using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PRN231.Application.Helpers;
using PRN231.Application.Services.AuthServices.Dtos;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Domain.Constants;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AuthServices;

public partial class AuthService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher,
    IUserIdentityService userIdentityService,
    IEmailSerivce emailSerivce
) : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IEmailSerivce _emailSerivce = emailSerivce;

    public async Task SignUpAsync(SignUpRequestDto request)
    {
        VerifyConfirmPassword(request.Password, request.PasswordConfirm);

        var isEmailExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email) is not null;
        if (isEmailExist)
        {
            throw new EmailExistException();
        }

        var newUser = _mapper.Map<User>(request);
        newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);
        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();
    }

    public async Task<LogInResponseDto> LoginAsync(LogInRequestDto request)
    {
        var user = await GetUserByEmail(request.Email);
        VerifyLogin(user, request.Password);

        var jwtModel = _mapper.Map<JwtModel>(user);
        var token = JwtHelpers.GenerateToken(jwtModel);

        var response = _mapper.Map<LogInResponseDto>(user);
        response.Token = token;
        return response;
    }

    public async Task PermanentlyDeleteUserAsync(PermanentlyDeleteRequestDto request)
    {
        var user = await GetUserByIdentity();
        VerifyLogin(user, request.Password);

        _unitOfWork.UserRepository.PermanentlyDelete(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdatePasswordAsync(UpdatePasswordRequestDto request)
    {
        VerifyConfirmPassword(request.Password, request.PasswordConfirm);
        var user = await GetUserByIdentity();
        VerifyLogin(user, request.OldPassword);

        UpdateUserPassword(user, request.Password);
        await _unitOfWork.CommitAsync();
    }

    public async Task RequestResetPasswordAsync(ForgotPasswordRequestDto request)
    {
        var user = await GetUserByEmail(request.Email);

        // Delete old token
        var oldTokens = await _unitOfWork.UserTokenRepository.ListByUserIdAsync(user.Id);
        _unitOfWork.UserTokenRepository.PermanentlyDeleteRange(oldTokens);

        var newToken = TokenHelpers.GenerateRandomToken(16);
        var newUserToken = new UserToken
        {
            Token = newToken,
            UserId = user.Id,
            ExpiredAt = DateTime.Now.AddSeconds(CacheConstants.Tokens.EXPIRY),
        };
        await _unitOfWork.UserTokenRepository.AddAsync(newUserToken);
        await _unitOfWork.CommitAsync();

        _emailSerivce.SendResetTokenEmail(user, newToken);
    }

    public async Task<bool> VerifyResetTokenAsync(VerifyResetTokenRequestDto request)
    {
        var _ = await GetValidUserToken(request.Token);
        return true;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var userToken = await GetValidUserToken(request.Token);
        VerifyConfirmPassword(request.Password, request.PasswordConfirm);

        var user = userToken.User;
        if (user is null)
        {
            throw new UserUnauthorizedException();
        }
        UpdateUserPassword(user, request.Password);
        _unitOfWork.UserTokenRepository.PermanentlyDelete(userToken);
        await _unitOfWork.CommitAsync();
    }
}
