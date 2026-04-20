using Microsoft.AspNetCore.SignalR;

namespace FixItQC.Api.Hubs;

public sealed class BulletinHub : Hub
{
    public Task Join(string group) => Groups.AddToGroupAsync(Context.ConnectionId, group);
}
