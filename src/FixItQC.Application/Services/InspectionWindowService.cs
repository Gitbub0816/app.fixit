namespace FixItQC.Application.Services;

public sealed class InspectionWindowService
{
    public string EvaluateWindow(DateOnly dueDate, DateOnly today)
    {
        var days = dueDate.DayNumber - today.DayNumber;
        if (days <= 5 && days >= 0) return "VALID_WINDOW";
        if (days < 0 && Math.Abs(days) <= 5) return "GRACE_PERIOD";
        return "NON_COMPLIANT";
    }
}
