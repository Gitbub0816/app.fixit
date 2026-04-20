using FixItQC.Application.Services;
using Xunit;

namespace FixItQC.UnitTests;

public sealed class InspectionWindowServiceTests
{
    [Fact]
    public void DueWithinFiveDays_IsValidWindow()
    {
        var svc = new InspectionWindowService();
        var result = svc.EvaluateWindow(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)), DateOnly.FromDateTime(DateTime.UtcNow));
        Assert.Equal("VALID_WINDOW", result);
    }
}
