using FixItQC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FixItQC.Api.BackgroundWorkers;

public sealed class IntegrationProcessingWorker(IServiceScopeFactory scopeFactory, ILogger<IntegrationProcessingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FixItQcDbContext>();
            var receivedCount = await db.IntegrationMessages.CountAsync(x => x.ProcessingState == "Received", stoppingToken);
            if (receivedCount > 0)
            {
                logger.LogInformation("IntegrationProcessingWorker found {Count} messages waiting", receivedCount);
            }
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}
