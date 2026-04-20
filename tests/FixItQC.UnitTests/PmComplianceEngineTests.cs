using FixItQC.Application.Compliance;
using FixItQC.Domain.Enums;
using Xunit;

namespace FixItQC.UnitTests;

public sealed class PmComplianceEngineTests
{
    [Fact]
    public void Evaluate_Within5Days_ShouldBeValid()
    {
        var engine = new PmComplianceEngine();
        var result = engine.Evaluate(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)), DateOnly.FromDateTime(DateTime.UtcNow));
        Assert.Equal(InspectionWindowStatus.Valid, result.WindowStatus);
    }
}
