﻿using AutoMapper;
using PRN231.Application.Helpers;
using PRN231.Application.Services.AuthServices.Dtos;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AuthServices;

public class AuthService(IMapper mapper, IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;    

    public async Task SignUp(SignUpRequestDto request)
    {
        if (request.Password != request.PasswordConfirm)
        {
            throw new Exception("Passwords must match!");
        }

        var isEmailExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email) is not null;
        if (isEmailExist)
        {
            throw new Exception("Email exist!");
        }

        var newUser = _mapper.Map<User>(request);
        newUser.Password = HashHelpers.HashPassword(request.Password);
        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();
    }

    public async Task<LogInResponseDto> Login(LogInRequestDto request)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email) 
            ?? throw new Exception("Not Found!");

        var passwordVerified = HashHelpers.VerifyPassword(request.Password, user.Password);
        if (!passwordVerified)
        {
            throw new Exception("Wrong credentials!");
        }

        var jwtModel = _mapper.Map<JwtModel>(user);
        var token = TokenHelpers.GenerateToken(jwtModel);

        var response = _mapper.Map<LogInResponseDto>(user);
        response.Token = token;
        return response;
    }
}