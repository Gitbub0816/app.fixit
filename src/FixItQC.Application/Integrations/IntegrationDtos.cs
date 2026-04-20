namespace FixItQC.Application.Integrations;

public sealed record RawIntegrationEnvelope(
    string SourceSystem,
    string MessageType,
    string SourceMessageId,
    string? IdempotencyKey,
    string Payload,
    string? AirlineCode);

public sealed record MappingSuggestion(string ExternalField, string SuggestedInternalField, decimal Confidence);

public sealed record NormalizedFlightPatch(
    string AirlineCode,
    string FlightNumber,
    string? TailNumber,
    string? Gate,
    string? AircraftType,
    DateTimeOffset? ScheduledArrivalUtc,
    DateTimeOffset? ScheduledDepartureUtc,
    bool? IsCancelled);
