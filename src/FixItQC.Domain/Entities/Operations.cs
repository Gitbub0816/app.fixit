using FixItQC.Domain.Common;
using FixItQC.Domain.Enums;

namespace FixItQC.Domain.Entities;

public sealed class UserProfile : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public PlatformRole Role { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? StationId { get; set; }
}

public sealed class Equipment : BaseEntity
{
    public Guid StationId { get; set; }
    public string EquipmentNumber { get; set; } = string.Empty;
    public string EquipmentType { get; set; } = "Tanker";
    public string CapacityClass { get; set; } = "Large";
    public bool IsLockedOut { get; set; }
    public string Status { get; set; } = "Active";
}

public sealed class RunningBalanceLedgerEvent : BaseEntity
{
    public Guid EquipmentId { get; set; }
    public Guid? OperatorId { get; set; }
    public Guid StationId { get; set; }
    public BalanceEventType EventType { get; set; }
    public decimal QuantityGallons { get; set; }
    public string? Reason { get; set; }
    public DateTimeOffset EventUtc { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class DispatchRecord : BaseEntity
{
    public Guid StationId { get; set; }
    public string DispatchKind { get; set; } = "Jet";
    public DispatchStatus Status { get; set; } = DispatchStatus.Pending;
    public string FlightOrServiceCode { get; set; } = string.Empty;
    public string GateOrLocation { get; set; } = string.Empty;
    public decimal RequestedGallons { get; set; }
    public decimal? ActualGallons { get; set; }
}

public sealed class InspectionRecord : BaseEntity
{
    public Guid EquipmentId { get; set; }
    public string TemplateName { get; set; } = "ATA 103 Walkaround";
    public bool Passed { get; set; }
    public bool HasException { get; set; }
    public string SignoffName { get; set; } = string.Empty;
}

public sealed class DamageRecord : BaseEntity
{
    public Guid EquipmentId { get; set; }
    public string ZoneId { get; set; } = "front-left";
    public string Severity { get; set; } = "Minor";
    public bool IsExistingDamage { get; set; }
    public string Notes { get; set; } = string.Empty;
}
