using FixItQC.Application.Integrations;
using Xunit;

namespace FixItQC.IntegrationTests;

public sealed class MappingEngineNormalizationTests
{
    [Fact]
    public void Normalize_ShouldParseTimeToUtc()
    {
        var service = new MappingEngineService();
        var patch = service.Normalize(new Dictionary<string, object?>
        {
            ["FlightNumber"] = "AC100",
            ["ScheduledDepartureUtc"] = "2026-04-20T10:00:00-05:00"
        }, "AC");

        Assert.Equal("AC100", patch.FlightNumber);
        Assert.NotNull(patch.ScheduledDepartureUtc);
    }
}
