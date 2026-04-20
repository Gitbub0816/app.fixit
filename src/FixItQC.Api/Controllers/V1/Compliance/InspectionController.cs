using FixItQC.Application.Compliance;
using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1.Compliance;

[ApiController]
[Route("api/v1/compliance/inspections")]
public sealed class InspectionController(FixItQcDbContext db) : ControllerBase
{
    [HttpGet("templates")]
    public async Task<IActionResult> Templates([FromQuery] bool? jigOnly = null)
    {
        var query = db.InspectionTemplates.AsQueryable();
        if (jigOnly == true) query = query.Where(x => x.IsJigTemplate);
        return Ok(await query.ToListAsync());
    }

    [HttpPost("templates")]
    public async Task<IActionResult> CreateTemplate([FromBody] InspectionTemplate template)
    {
        db.InspectionTemplates.Add(template);
        await db.SaveChangesAsync();
        return Created($"/api/v1/compliance/inspections/templates/{template.Id}", template);
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody] InspectionExecution execution, [FromQuery] bool criticalFailure = false)
    {
        var result = new AtaInspectionService().Finalize(execution.Passed, criticalFailure, execution.FailureSummary);
        execution.Passed = result.Passed;

        db.InspectionExecutions.Add(execution);
        await db.SaveChangesAsync();

        if (result.WorkOrderRequired)
        {
            db.WorkOrders.Add(new WorkOrder
            {
                StationId = Guid.Empty,
                EquipmentId = execution.EquipmentId,
                Title = "Auto-generated from failed ATA/QC inspection",
                Priority = "High",
                Status = "Open",
                CreatedFromInspectionExecutionId = execution.Id
            });
            await db.SaveChangesAsync();
        }

        return Ok(result);
    }

    [HttpGet("jig-state")]
    public IActionResult JigState([FromQuery] bool jigEnabled)
    {
        var svc = new JigComplianceService();
        return Ok(new { loadJigTemplates = svc.ShouldLoadJigTemplates(jigEnabled), label = svc.BuildDashboardLabel(jigEnabled) });
    }
}
