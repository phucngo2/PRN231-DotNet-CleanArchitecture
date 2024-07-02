﻿using PRN231.Application.Services.AuthServices.Dtos;

namespace PRN231.Application.Services.AuthServices;

public interface IAuthService
{
    public Task SignUp(SignUpRequestDto request);
    public Task<LogInResponseDto> Login(LogInRequestDto request);
    public Task PermanentlyDeleteUser(PermanentlyDeleteRequestDto request);
    public Task UpdatePassword(UpdatePasswordRequestDto request);
}
