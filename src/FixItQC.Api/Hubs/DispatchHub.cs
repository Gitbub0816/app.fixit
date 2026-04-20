using Microsoft.AspNetCore.SignalR;

namespace FixItQC.Api.Hubs;

public sealed class DispatchHub : Hub
{
    public Task JoinScopeGroup(string level, string scopeId) => Groups.AddToGroupAsync(Context.ConnectionId, $"{level}:{scopeId}");
    public Task LeaveScopeGroup(string level, string scopeId) => Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{level}:{scopeId}");
}
