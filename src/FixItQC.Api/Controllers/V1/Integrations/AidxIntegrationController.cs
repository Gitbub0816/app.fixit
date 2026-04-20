using FixItQC.Application.Integrations;
using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1.Integrations;

[ApiController]
[Route("api/v1/integrations/aidx")]
public sealed class AidxIntegrationController(FixItQcDbContext db) : ControllerBase
{
    [HttpPost("messages")]
    public async Task<IActionResult> Ingest([FromBody] string xmlPayload, [FromHeader(Name = "X-Source-Message-Id")] string sourceMessageId, [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey)
    {
        if (await db.IntegrationMessages.AnyAsync(x => x.SourceMessageId == sourceMessageId && x.SourceSystem == "AIDX"))
            return Ok(new { duplicate = true, sourceMessageId });

        var envelope = new AidxIngestionService().ParseEnvelope(xmlPayload, sourceMessageId, idempotencyKey);
        db.IntegrationMessages.Add(new IntegrationMessage
        {
            SourceSystem = envelope.SourceSystem,
            MessageType = envelope.MessageType,
            SourceMessageId = envelope.SourceMessageId,
            IdempotencyKey = envelope.IdempotencyKey,
            RawPayload = envelope.Payload,
            AirlineCode = envelope.AirlineCode,
            ProcessingState = "Received"
        });
        await db.SaveChangesAsync();

        return Accepted(new { stored = true, sourceMessageId });
    }
}
