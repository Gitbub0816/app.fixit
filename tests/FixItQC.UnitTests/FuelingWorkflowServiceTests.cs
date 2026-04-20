using FixItQC.Application.DTOs;
using FixItQC.Application.Services;

namespace FixItQC.UnitTests;

public sealed class FuelingWorkflowServiceTests
{
    public void Validate_WidebodyImbalance_ShouldFail()
    {
        var svc = new FuelingWorkflowService();
        var draft = new FuelingTicketDraft(
            "AC900",
            "A330",
            "N88",
            new List<FuelTankInput>
            {
                new("left-wing", 6000),
                new("right-wing", 4200)
            },
            100,
            5300,
            6.7m,
            DateTimeOffset.UtcNow.AddMinutes(10),
            DateTimeOffset.UtcNow);

        _ = svc.ValidateAndScore(draft, 5200);
    }
}
