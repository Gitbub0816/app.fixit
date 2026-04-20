using FixItQC.Domain.Entities;

namespace FixItQC.Application.Services;

public sealed class RunningBalanceService
{
    public decimal ComputeBalance(IEnumerable<RunningBalanceLedgerEvent> events)
    {
        return events.OrderBy(e => e.EventUtc).Sum(e => e.QuantityGallons);
    }
}
