using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PRN231.Application.Helpers;
using PRN231.Application.Services.AuthServices.Dtos;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Exceptions.User;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AuthServices;

public partial class AuthService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher,
    IUserIdentityService userIdentityService) : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IUserIdentityService _userIdentityService = userIdentityService;

    public async Task SignUp(SignUpRequestDto request)
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

    public async Task<LogInResponseDto> Login(LogInRequestDto request)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email)
            ?? throw new UserNotFoundException();

        VerifyLogin(user, request.Password);

        var jwtModel = _mapper.Map<JwtModel>(user);
        var token = TokenHelpers.GenerateToken(jwtModel);

        var response = _mapper.Map<LogInResponseDto>(user);
        response.Token = token;
        return response;
    }

    public async Task PermanentlyDeleteUser(PermanentlyDeleteRequestDto request)
    {
        var user = await GetUserByIdentity();
        VerifyLogin(user, request.Password);

        _unitOfWork.UserRepository.PermanentlyDelete(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdatePassword(UpdatePasswordRequestDto request)
    {
        VerifyConfirmPassword(request.Password, request.PasswordConfirm);
        var user = await GetUserByIdentity();
        VerifyLogin(user, request.OldPassword);

        user.Password = _passwordHasher.HashPassword(user, request.Password);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }
}
