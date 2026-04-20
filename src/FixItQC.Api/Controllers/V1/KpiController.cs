using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1;

[ApiController]
[Route("api/v1/kpi")]
public sealed class KpiController(FixItQcDbContext db) : ControllerBase
{
    [HttpGet("dashboard/station/{stationId:guid}")]
    public async Task<IActionResult> Station(Guid stationId)
        => Ok(await db.KpiSnapshots.Where(x => x.StationId == stationId).OrderByDescending(x => x.SnapshotDate).Take(30).ToListAsync());

    [HttpGet("leaderboard/stations")]
    public async Task<IActionResult> StationsLeaderboard()
        => Ok(await db.KpiSnapshots.Where(x => x.StationId != null)
            .OrderByDescending(x => (x.OnTimeFuelingRate + x.PmCompletionRate + x.QcComplianceRate + x.DispatchCompletionRate) / 4m)
            .Take(10)
            .ToListAsync());
}
