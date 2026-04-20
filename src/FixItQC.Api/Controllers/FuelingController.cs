using FixItQC.Application.DTOs;
using FixItQC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FuelingController : ControllerBase
{
    private readonly FuelingWorkflowService _workflow = new();

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] FuelingTicketDraft draft, [FromQuery] decimal plannedGallons)
    {
        var result = _workflow.ValidateAndScore(draft, plannedGallons);
        return Ok(result);
    }
}
