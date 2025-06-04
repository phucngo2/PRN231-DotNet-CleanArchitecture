using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.UserServices;
using PRN231.Domain.Extensions;


namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var res = await _userService.GetCurrentUserAsync();
        return res.ToResult();
    }
}
