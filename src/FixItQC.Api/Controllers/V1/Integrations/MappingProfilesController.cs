using FixItQC.Application.Integrations;
using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1.Integrations;

[ApiController]
[Route("api/v1/integrations/mappings")]
public sealed class MappingProfilesController(FixItQcDbContext db) : ControllerBase
{
    [HttpPost("suggestions")]
    public IActionResult Suggestions([FromBody] string payload)
    {
        var suggestions = new MappingEngineService().SuggestMappings(payload);
        return Ok(suggestions);
    }

    [HttpPost("profiles")]
    public async Task<IActionResult> CreateProfile([FromBody] MappingProfile profile)
    {
        db.MappingProfiles.Add(profile);
        await db.SaveChangesAsync();
        return Created($"/api/v1/integrations/mappings/profiles/{profile.Id}", profile);
    }

    [HttpGet("profiles")]
    public async Task<IActionResult> ListProfiles() => Ok(await db.MappingProfiles.Include(x => x.Rules).ToListAsync());
}
