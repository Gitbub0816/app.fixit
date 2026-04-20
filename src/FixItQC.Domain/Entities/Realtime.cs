using FixItQC.Domain.Common;

namespace FixItQC.Domain.Entities;

public sealed class CommsMessage : BaseEntity
{
    public Guid SenderUserId { get; set; }
    public string ChannelScope { get; set; } = "Station";
    public Guid ScopeId { get; set; }
    public bool IsVoice { get; set; }
    public string TranscriptText { get; set; } = string.Empty;
    public string? AudioBlobPath { get; set; }
}

public sealed class SafetyBulletinAcknowledgement : BaseEntity
{
    public Guid BulletinId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset AcknowledgedUtc { get; set; }
}

public sealed class KpiSnapshot : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? StationId { get; set; }
    public decimal OnTimeFuelingRate { get; set; }
    public decimal PmCompletionRate { get; set; }
    public decimal QcComplianceRate { get; set; }
    public decimal DispatchCompletionRate { get; set; }
    public DateOnly SnapshotDate { get; set; }
}

public sealed class Commendation : BaseEntity
{
    public Guid GivenByUserId { get; set; }
    public Guid RecipientUserId { get; set; }
    public Guid RegionId { get; set; }
    public string Message { get; set; } = string.Empty;
}
