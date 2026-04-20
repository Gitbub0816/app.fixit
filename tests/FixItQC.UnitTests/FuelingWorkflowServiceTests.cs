using FixItQC.Application.DTOs;
using FixItQC.Application.Services;
using Xunit;

namespace FixItQC.UnitTests;

public sealed class FuelingWorkflowServiceTests
{
    [Fact]
    public void Validate_WidebodyImbalance_ShouldFail()
    {
        var svc = new FuelingWorkflowService();
        var draft = new FuelingTicketDraft(
            "AC900",
            "A330",
            "N88",
            [new FuelTankInput("left-wing", 6000), new FuelTankInput("right-wing", 4200)],
            100,
            5300,
            6.7m,
            DateTimeOffset.UtcNow.AddMinutes(10),
            DateTimeOffset.UtcNow);

        var result = svc.ValidateAndScore(draft, 5200);
        Assert.False(result.IsValid);
    }
}
