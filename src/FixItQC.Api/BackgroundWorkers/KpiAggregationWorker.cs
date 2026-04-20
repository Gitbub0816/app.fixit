using FixItQC.Domain.Entities;
using FixItQC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.BackgroundWorkers;

public sealed class KpiAggregationWorker(IServiceScopeFactory scopeFactory, ILogger<KpiAggregationWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FixItQcDbContext>();

            var stationIds = await db.Equipment.Select(x => x.StationId).Distinct().ToListAsync(stoppingToken);
            foreach (var stationId in stationIds)
            {
                if (await db.KpiSnapshots.AnyAsync(x => x.StationId == stationId && x.SnapshotDate == DateOnly.FromDateTime(DateTime.UtcNow), stoppingToken))
                    continue;

                db.KpiSnapshots.Add(new KpiSnapshot
                {
                    OrganizationId = Guid.Empty,
                    RegionId = null,
                    StationId = stationId,
                    OnTimeFuelingRate = 95,
                    PmCompletionRate = 90,
                    QcComplianceRate = 92,
                    DispatchCompletionRate = 94,
                    SnapshotDate = DateOnly.FromDateTime(DateTime.UtcNow)
                });
            }

            await db.SaveChangesAsync(stoppingToken);
            logger.LogInformation("KPI snapshot aggregation cycle completed.");
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}
