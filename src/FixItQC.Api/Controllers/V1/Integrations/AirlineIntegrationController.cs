using FixItQC.Application.Integrations;
using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.Controllers.V1.Integrations;

[ApiController]
[Route("api/v1/integrations/airlines/{airlineCode}")]
public sealed class AirlineIntegrationController(FixItQcDbContext db, ILogger<AirlineIntegrationController> logger) : ControllerBase
{
    [HttpPost("flights:upsert")]
    public async Task<IActionResult> UpsertFlight(string airlineCode, [FromBody] Dictionary<string, object?> payload, [FromHeader(Name = "X-Source-Message-Id")] string sourceMessageId)
    {
        await StoreRaw(airlineCode, sourceMessageId, payload, "FlightUpsert");

        var patch = new MappingEngineService().Normalize(payload, airlineCode);
        var existing = await db.NormalizedFlightUpdates.FirstOrDefaultAsync(x => x.AirlineCode == airlineCode && x.FlightNumber == patch.FlightNumber);

        if (existing is null)
        {
            db.NormalizedFlightUpdates.Add(new NormalizedFlightUpdate
            {
                AirlineCode = airlineCode,
                FlightNumber = patch.FlightNumber,
                TailNumber = patch.TailNumber,
                Gate = patch.Gate,
                AircraftType = patch.AircraftType,
                ScheduledArrivalUtc = patch.ScheduledArrivalUtc,
                ScheduledDepartureUtc = patch.ScheduledDepartureUtc,
                IsCancelled = patch.IsCancelled ?? false,
                SourceMessageId = sourceMessageId
            });
        }
        else
        {
            existing.TailNumber = patch.TailNumber ?? existing.TailNumber;
            existing.Gate = patch.Gate ?? existing.Gate;
            existing.AircraftType = patch.AircraftType ?? existing.AircraftType;
            existing.ScheduledArrivalUtc = patch.ScheduledArrivalUtc ?? existing.ScheduledArrivalUtc;
            existing.ScheduledDepartureUtc = patch.ScheduledDepartureUtc ?? existing.ScheduledDepartureUtc;
            existing.IsCancelled = patch.IsCancelled ?? existing.IsCancelled;
            existing.SourceMessageId = sourceMessageId;
            existing.UpdatedUtc = DateTimeOffset.UtcNow;
        }

        await db.SaveChangesAsync();
        return Ok(new { upserted = true, patch.FlightNumber });
    }

    [HttpPost("flights:bulk-upsert")]
    public async Task<IActionResult> BulkUpsert(string airlineCode, [FromBody] List<Dictionary<string, object?>> payloads)
    {
        foreach (var payload in payloads)
        {
            var sourceMessageId = payload.GetValueOrDefault("sourceMessageId")?.ToString() ?? Guid.NewGuid().ToString("N");
            await StoreRaw(airlineCode, sourceMessageId, payload, "FlightBulkUpsert");
        }
        await db.SaveChangesAsync();
        return Accepted(new { accepted = payloads.Count });
    }

    [HttpPost("assignments:upsert")]
    public IActionResult UpsertAssignment(string airlineCode, [FromBody] Dictionary<string, object?> assignment)
    {
        logger.LogInformation("Assignment upsert for {Airline}: {@Assignment}", airlineCode, assignment);
        return Ok(new { airlineCode, upserted = true });
    }

    private Task StoreRaw(string airlineCode, string sourceMessageId, Dictionary<string, object?> payload, string type)
    {
        db.IntegrationMessages.Add(new IntegrationMessage
        {
            SourceSystem = "AirlineRest",
            AirlineCode = airlineCode,
            SourceMessageId = sourceMessageId,
            MessageType = type,
            RawPayload = System.Text.Json.JsonSerializer.Serialize(payload),
            ProcessingState = "Received"
        });

        return Task.CompletedTask;
    }
}
