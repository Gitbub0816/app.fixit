using FixItQC.Domain.Common;

namespace FixItQC.Domain.Entities;

public sealed class PmTemplate : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Cadence { get; set; } = "Monthly"; // Weekly, Monthly, Quarterly, SemiAnnual, Annual
    public bool Ata103Aligned { get; set; } = true;
}

public sealed class PmSchedule : BaseEntity
{
    public Guid TemplateId { get; set; }
    public Guid EquipmentId { get; set; }
    public DateOnly DueDate { get; set; }
    public bool RequiredBeforeReturnToService { get; set; }
}

public sealed class SafetyBulletin : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTimeOffset? ExpiresUtc { get; set; }
    public string ScopeLevel { get; set; } = "Station"; // Station, Region, Organization, Platform
    public Guid? ScopeId { get; set; }
    public string ApprovalState { get; set; } = "Draft"; // Draft, Submitted, Approved
}

public sealed class DispatchFlight : BaseEntity
{
    public string FlightCode { get; set; } = string.Empty;
    public string AircraftType { get; set; } = string.Empty;
    public string Gate { get; set; } = string.Empty;
    public DateTimeOffset ArrivalUtc { get; set; }
    public DateTimeOffset DepartureUtc { get; set; }
    public bool UseVisualTimeline { get; set; } = true;
}
