using FixItQC.Application.Compliance;
using Xunit;

namespace FixItQC.UnitTests;

public sealed class AtaInspectionServiceTests
{
    [Fact]
    public void CriticalFailure_ShouldRequireWorkOrder()
    {
        var svc = new AtaInspectionService();
        var result = svc.Finalize(false, true, "Filter separator failed");
        Assert.True(result.WorkOrderRequired);
    }
}
