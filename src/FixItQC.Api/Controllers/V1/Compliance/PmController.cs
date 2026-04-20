using FixItQC.Application.Compliance;
using FixItQC.Domain.Entities;
using FixItQC.Domain.Enums;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1.Compliance;

[ApiController]
[Route("api/v1/compliance/pm")]
public sealed class PmController(FixItQcDbContext db) : ControllerBase
{
    [HttpGet("schedules")]
    public async Task<IActionResult> List([FromQuery] Guid? stationId = null)
    {
        var query = db.PmSchedules.AsQueryable();
        if (stationId.HasValue)
        {
            var equipmentIds = await db.Equipment.Where(x => x.StationId == stationId).Select(x => x.Id).ToListAsync();
            query = query.Where(x => equipmentIds.Contains(x.EquipmentId));
        }

        return Ok(await query.OrderBy(x => x.DueDate).ToListAsync());
    }

    [HttpGet("due-eval")]
    public IActionResult DueEval([FromQuery] DateOnly dueDate, [FromQuery] DateOnly today)
        => Ok(new PmComplianceEngine().Evaluate(dueDate, today));

    [HttpPost("templates")]
    public async Task<IActionResult> AddTemplate([FromBody] PmTemplate template)
    {
        db.PmTemplates.Add(template);
        await db.SaveChangesAsync();
        return Created($"/api/v1/compliance/pm/templates/{template.Id}", template);
    }

    [HttpGet("cadence/next")]
    public IActionResult Next([FromQuery] DateOnly completedDate, [FromQuery] InspectionCadence cadence)
        => Ok(new { nextDue = new PmComplianceEngine().NextDue(completedDate, cadence) });
}
