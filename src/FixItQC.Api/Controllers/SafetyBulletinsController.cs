using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SafetyBulletinsController : ControllerBase
{
    [HttpGet]
    public IActionResult List() => Ok(new[]
    {
        new { Title = "Water Detection Procedure Reminder", Scope = "Station", ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5) },
        new { Title = "QC Sampling Update", Scope = "Organization", ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14) }
    });
}
