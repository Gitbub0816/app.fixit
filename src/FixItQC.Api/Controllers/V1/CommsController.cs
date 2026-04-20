using FixItQC.Application.Comms;
using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1;

[ApiController]
[Route("api/v1/comms")]
public sealed class CommsController(FixItQcDbContext db) : ControllerBase
{
    [HttpPost("transmit")]
    public async Task<IActionResult> Transmit([FromBody] RadioTransmissionRequest request)
    {
        var result = new RadioCommsService().Process(request);
        db.CommsMessages.Add(new CommsMessage
        {
            SenderUserId = request.SenderUserId,
            ChannelScope = request.ChannelScope,
            ScopeId = request.ScopeId,
            IsVoice = request.IsVoice,
            TranscriptText = result.Transcript
        });
        await db.SaveChangesAsync();
        return Ok(result);
    }

    [HttpPost("users/{userId:guid}/background-audio")]
    public async Task<IActionResult> ToggleBackgroundAudio(Guid userId, [FromQuery] bool allow)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null) return NotFound();

        user.AllowBackgroundAudio = allow;
        user.UpdatedUtc = DateTimeOffset.UtcNow;
        await db.SaveChangesAsync();
        return Ok(new { userId, allowBackgroundAudio = allow });
    }
}
