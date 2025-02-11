using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PRN231.Application.Services.UserIdentityServices;

public class UserIdentityService(IHttpContextAccessor httpContextAccessor) : IUserIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public int? GetUserId()
    {
        var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdStr == null) return null;

        _ = int.TryParse(userIdStr, out int userId);
        return userId;
    }
}
