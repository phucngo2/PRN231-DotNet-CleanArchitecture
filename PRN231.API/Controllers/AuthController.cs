﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.AuthServices;
using PRN231.Application.Services.AuthServices.Dtos;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request)
    {
        await _authService.SignUpAsync(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequestDto request)
    {
        var res = await _authService.LoginAsync(request);
        return Ok(res);
    }

    [HttpPost("permanently-delete")]
    [Authorize]
    public async Task<IActionResult> PermanentlyDelete([FromBody] PermanentlyDeleteRequestDto request)
    {
        await _authService.PermanentlyDeleteUserAsync(request);
        return Ok();
    }

    [HttpPut("password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto request)
    {
        await _authService.UpdatePasswordAsync(request);
        return Ok();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasword([FromBody] ForgotPasswordRequestDto request)
    {
        await _authService.RequestResetPasswordAsync(request);
        return Ok();
    }

    [HttpPost("verify-reset-token")]
    public async Task<IActionResult> VeifyResetToken([FromBody] VerifyResetTokenRequestDto request)
    {
        await _authService.VerifyResetTokenAsync(request);
        return Ok();
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPasword([FromBody] ResetPasswordRequestDto request)
    {
        await _authService.ResetPasswordAsync(request);
        return Ok();
    }
}
