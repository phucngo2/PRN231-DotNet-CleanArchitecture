﻿using AutoMapper;
using PRN231.Application.Helpers;
using PRN231.Application.Services.AuthServices.Dtos;
using PRN231.Application.Services.UserIdentityServices;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Auth;
using PRN231.Domain.Exceptions.User;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AuthServices;

public class AuthService(IMapper mapper, IUnitOfWork unitOfWork, IUserIdentityService userIdentityService) : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdentityService _userIdentityService = userIdentityService;

    public async Task SignUp(SignUpRequestDto request)
    {
        if (request.Password != request.PasswordConfirm)
        {
            throw new PasswordMustMatchException();
        }

        var isEmailExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email) is not null;
        if (isEmailExist)
        {
            throw new EmailExistException();
        }

        var newUser = _mapper.Map<User>(request);
        newUser.Password = HashHelpers.HashPassword(request.Password);
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
        var userId = _userIdentityService.GetUserId()
            ?? throw new UserUnauthorizedException();

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId)
            ?? throw new UserNotFoundException();

        VerifyLogin(user, request.Password);

        _unitOfWork.UserRepository.Delete(user);
        await _unitOfWork.CommitAsync();
    }

    private static void VerifyLogin(User user, string password)
    {
        var passwordVerified = HashHelpers.VerifyPassword(password, user.Password);
        if (!passwordVerified)
        {
            throw new WrongCredentialsException();
        }
    }
}
