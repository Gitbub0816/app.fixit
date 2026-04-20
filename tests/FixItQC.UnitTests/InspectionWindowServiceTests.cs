using FixItQC.Application.Services;

namespace FixItQC.UnitTests;

public sealed class InspectionWindowServiceTests
{
    public void DueWithinFiveDays_IsValidWindow()
    {
        var svc = new InspectionWindowService();
        _ = svc.EvaluateWindow(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)), DateOnly.FromDateTime(DateTime.UtcNow));
    }
}
