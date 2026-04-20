namespace FixItQC.Application.Compliance;

public sealed record AtaInspectionResult(bool Passed, bool WorkOrderRequired, string? FailureSummary);

public sealed class AtaInspectionService
{
    public AtaInspectionResult Finalize(bool passed, bool hasCriticalFailure, string? failureSummary)
    {
        if (passed && !hasCriticalFailure) return new AtaInspectionResult(true, false, null);
        return new AtaInspectionResult(false, true, failureSummary ?? "Critical ATA/QC finding");
    }
}
