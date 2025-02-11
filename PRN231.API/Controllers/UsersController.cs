using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.API.Common;
using PRN231.Application.Services.UserServices;


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
