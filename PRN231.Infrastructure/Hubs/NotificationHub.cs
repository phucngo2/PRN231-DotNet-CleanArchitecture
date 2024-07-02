using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PRN231.Domain.Constants;
using PRN231.Domain.Enums;

namespace PRN231.Infrastructure.Hubs;

[Authorize(Roles = UserRoles.ADMIN)]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        try
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserRoles.ADMIN);
        }
        catch (Exception)
        {
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {

        try
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserRoles.ADMIN);
        }
        catch (Exception)
        {
        }

        await base.OnConnectedAsync();
    }

    public async Task SendNotification(string notification)
    {
        await Clients.Group(UserRoles.ADMIN).SendAsync(HubConstants.Notification, notification, DateTime.Now);
    }
}
