using FixItQC.Domain.Enums;

namespace FixItQC.Application.Compliance;

public sealed record PmDueEvaluation(DateOnly DueDate, InspectionWindowStatus WindowStatus, bool IsOverdue, int DaysDelta);

public sealed class PmComplianceEngine
{
    public PmDueEvaluation Evaluate(DateOnly dueDate, DateOnly today)
    {
        var delta = dueDate.DayNumber - today.DayNumber;
        var window = delta switch
        {
            >= 0 and <= 5 => InspectionWindowStatus.Valid,
            < 0 when Math.Abs(delta) <= 5 => InspectionWindowStatus.Grace,
            _ => InspectionWindowStatus.NonCompliant
        };

        return new PmDueEvaluation(dueDate, window, today > dueDate, delta);
    }

    public DateOnly NextDue(DateOnly completedDate, InspectionCadence cadence)
        => cadence switch
        {
            InspectionCadence.Daily => completedDate.AddDays(1),
            InspectionCadence.Weekly => completedDate.AddDays(7),
            InspectionCadence.Monthly => completedDate.AddMonths(1),
            InspectionCadence.Quarterly => completedDate.AddMonths(3),
            InspectionCadence.SemiAnnual => completedDate.AddMonths(6),
            InspectionCadence.Annual => completedDate.AddYears(1),
            _ => completedDate.AddMonths(1)
        };
}
