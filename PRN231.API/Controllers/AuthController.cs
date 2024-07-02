using Microsoft.AspNetCore.Authorization;
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
        await _authService.SignUp(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequestDto request)
    {
        var res = await _authService.Login(request);
        return Ok(res);
    }

    [HttpPost("permanently-delete")]
    [Authorize]
    public async Task<IActionResult> PermanentlyDelete([FromBody] PermanentlyDeleteRequestDto request)
    {
        await _authService.PermanentlyDeleteUser(request);
        return Ok();
    }

    [HttpPut("password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto request)
    {
        await _authService.UpdatePassword(request);
        return Ok();
    }
}
