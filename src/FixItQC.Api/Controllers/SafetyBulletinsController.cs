using FixItQC.Application.Services;
using FixItQC.Domain.Entities;
using FixItQC.Domain.Enums;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SafetyBulletinsController(FixItQcDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List() => Ok(await db.SafetyBulletins.OrderByDescending(x => x.CreatedUtc).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SafetyBulletin bulletin)
    {
        db.SafetyBulletins.Add(bulletin);
        await db.SaveChangesAsync();
        return Created($"/api/safetybulletins/{bulletin.Id}", bulletin);
    }

    [HttpPost("{id:guid}/submit-upward")]
    public async Task<IActionResult> SubmitUpward(Guid id, [FromQuery] PlatformRole actorRole, [FromQuery] PlatformRole approverRole)
    {
        var service = new BulletinApprovalService();
        if (!service.CanSubmitUpward(actorRole, approverRole)) return BadRequest("Invalid approval chain");

        var bulletin = await db.SafetyBulletins.FirstOrDefaultAsync(x => x.Id == id);
        if (bulletin is null) return NotFound();

        bulletin.ApprovalState = "Submitted";
        bulletin.UpdatedUtc = DateTimeOffset.UtcNow;
        await db.SaveChangesAsync();
        return Ok(bulletin);
    }

    [HttpPost("{id:guid}/acknowledge")]
    public async Task<IActionResult> Acknowledge(Guid id, [FromQuery] Guid userId)
    {
        db.BulletinAcknowledgements.Add(new SafetyBulletinAcknowledgement
        {
            BulletinId = id,
            UserId = userId,
            AcknowledgedUtc = DateTimeOffset.UtcNow
        });
        await db.SaveChangesAsync();
        return Ok(new { acknowledged = true });
    }
}
