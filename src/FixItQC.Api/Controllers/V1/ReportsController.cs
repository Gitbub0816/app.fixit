using FixItQC.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FixItQC.Api.Controllers.V1;

[ApiController]
[Route("api/v1/reports")]
public sealed class ReportsController(IPdfReportRenderer renderer) : ControllerBase
{
    [HttpGet("work-orders/{id:guid}.pdf")]
    public IActionResult WorkOrder(Guid id)
    {
        var bytes = renderer.RenderWorkOrderPdf(new { WorkOrderId = id, GeneratedUtc = DateTimeOffset.UtcNow });
        return File(bytes, "application/pdf", $"work-order-{id:N}.pdf");
    }

    [HttpGet("audits/{id:guid}.pdf")]
    public IActionResult Audit(Guid id)
    {
        var bytes = renderer.RenderAuditPdf(new { AuditId = id, GeneratedUtc = DateTimeOffset.UtcNow });
        return File(bytes, "application/pdf", $"audit-{id:N}.pdf");
    }

    [HttpGet("damage/{id:guid}.pdf")]
    public IActionResult Damage(Guid id)
    {
        var bytes = renderer.RenderDamagePdf(new { DamageId = id, GeneratedUtc = DateTimeOffset.UtcNow });
        return File(bytes, "application/pdf", $"damage-{id:N}.pdf");
    }
}
