using Microsoft.AspNetCore.SignalR;

namespace EquipmentRentalSystem_web.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
