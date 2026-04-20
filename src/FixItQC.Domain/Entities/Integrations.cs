using FixItQC.Domain.Common;

namespace FixItQC.Domain.Entities;

public sealed class IntegrationMessage : BaseEntity
{
    public string SourceSystem { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public string? AirlineCode { get; set; }
    public string SourceMessageId { get; set; } = string.Empty;
    public string? IdempotencyKey { get; set; }
    public string RawPayload { get; set; } = string.Empty;
    public string ProcessingState { get; set; } = "Received";
    public string? ErrorSummary { get; set; }
}

public sealed class MappingProfile : BaseEntity
{
    public string SourceSystem { get; set; } = string.Empty;
    public string ProfileName { get; set; } = string.Empty;
    public string Version { get; set; } = "v1";
    public bool IsActive { get; set; } = true;
    public ICollection<MappingRule> Rules { get; set; } = new List<MappingRule>();
}

public sealed class MappingRule : BaseEntity
{
    public Guid ProfileId { get; set; }
    public string ExternalField { get; set; } = string.Empty;
    public string InternalField { get; set; } = string.Empty;
    public string TransformKind { get; set; } = "None";
    public string? TransformConfigJson { get; set; }
}

public sealed class NormalizedFlightUpdate : BaseEntity
{
    public string AirlineCode { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string? TailNumber { get; set; }
    public string? Gate { get; set; }
    public string? AircraftType { get; set; }
    public DateTimeOffset? ScheduledArrivalUtc { get; set; }
    public DateTimeOffset? ScheduledDepartureUtc { get; set; }
    public bool IsCancelled { get; set; }
    public string SourceMessageId { get; set; } = string.Empty;
}
