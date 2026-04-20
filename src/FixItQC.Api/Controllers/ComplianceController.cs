using FixItQC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ComplianceController : ControllerBase
{
    [HttpGet("inspection-window")]
    public IActionResult InspectionWindow([FromQuery] DateOnly dueDate, [FromQuery] DateOnly today)
    {
        var service = new InspectionWindowService();
        return Ok(new { status = service.EvaluateWindow(dueDate, today), dueDate, today });
    }

    [HttpGet("on-time")]
    public IActionResult OnTime([FromQuery] DateTimeOffset departureUtc, [FromQuery] DateTimeOffset completedUtc)
    {
        return Ok(new { status = FuelingWorkflowService.ComputeOnTime(departureUtc, completedUtc) });
    }
}
