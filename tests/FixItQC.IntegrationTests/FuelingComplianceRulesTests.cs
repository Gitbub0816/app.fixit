using FixItQC.Application.DTOs;
using FixItQC.Application.Services;
using Xunit;

namespace FixItQC.IntegrationTests;

public sealed class FuelingComplianceRulesTests
{
    [Fact]
    public void Should_Return_Red_When_Late()
    {
        var draft = new FuelingTicketDraft(
            "AC102", "737", "N12",
            [new FuelTankInput("left-wing", 2000), new FuelTankInput("right-wing", 1800)],
            100, 5000, 6.7m,
            DateTimeOffset.UtcNow,
            DateTimeOffset.UtcNow.AddMinutes(1));

        var result = new FuelingWorkflowService().ValidateAndScore(draft, 5000);
        Assert.Equal("DELAY", result.OnTimeStatus);
    }
}
