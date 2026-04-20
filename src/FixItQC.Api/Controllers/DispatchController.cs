using FixItQC.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DispatchController : ControllerBase
{
    [HttpGet("board")]
    public IActionResult GetBoard([FromQuery] DispatchViewMode viewMode = DispatchViewMode.VisualTimeline, [FromQuery] DispatchServiceType filter = DispatchServiceType.Both)
    {
        var data = new[]
        {
            new { Id = Guid.NewGuid(), Kind = DispatchServiceType.Jet, Flight = "AC102", Gate = "B14", Status = ExtendedDispatchStatus.Assigned, RequestedGallons = 5200, Arrival="13:00", Departure="13:55" },
            new { Id = Guid.NewGuid(), Kind = DispatchServiceType.Gse, Flight = "TUG-07", Gate = "RAMP-2", Status = ExtendedDispatchStatus.Pending, RequestedGallons = 180, Arrival="13:10", Departure="13:40" }
        }.Where(x => filter == DispatchServiceType.Both || x.Kind == filter);

        return Ok(new { viewMode, filter, rows = data });
    }
}
