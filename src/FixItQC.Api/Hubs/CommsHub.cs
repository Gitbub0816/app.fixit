using FixItQC.Application.Comms;
using Microsoft.AspNetCore.SignalR;

namespace FixItQC.Api.Hubs;

public sealed class CommsHub : Hub
{
    private readonly RadioCommsService _service;

    public CommsHub(RadioCommsService service) => _service = service;

    public async Task PushToTalk(RadioTransmissionRequest request)
    {
        var result = _service.Process(request);
        await Clients.Group($"{request.ChannelScope}:{request.ScopeId}").SendAsync("radio:message", result);
    }
}
