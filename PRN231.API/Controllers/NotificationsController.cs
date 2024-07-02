using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRN231.Domain.Constants;
using PRN231.Domain.Enums;
using PRN231.Infrastructure.Hubs;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(IHubContext<NotificationHub> hubContext) : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    [HttpGet("notify-admin")]
    public async Task<IActionResult> NotifyAdmin()
    {
        await _hubContext.Clients.Group(UserRoles.ADMIN).SendAsync(HubConstants.Notification, $"Hello Sekai!", DateTime.Now);
        return Ok();
    }
}
