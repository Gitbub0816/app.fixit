using FixItQC.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DispatchController : ControllerBase
{
    [HttpGet("board")]
    public IActionResult GetBoard()
    {
        return Ok(new[]
        {
            new { Id = Guid.NewGuid(), Kind = "Jet", Flight = "AC102", Gate = "B14", Status = DispatchStatus.Assigned.ToString(), RequestedGallons = 5200 },
            new { Id = Guid.NewGuid(), Kind = "GSE", Flight = "TUG-07", Gate = "RAMP-2", Status = DispatchStatus.Pending.ToString(), RequestedGallons = 180 }
        });
    }
}
