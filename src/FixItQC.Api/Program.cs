using FixItQC.Api.BackgroundWorkers;
using FixItQC.Api.Hubs;
using FixItQC.Application.Abstractions;
using FixItQC.Application.Authorization;
using FixItQC.Application.Comms;
using FixItQC.Application.Services;
using FixItQC.Infrastructure.Diagnostics;
using FixItQC.Infrastructure.Pdf;
using FixItQC.Infrastructure.Persistence;
using FixItQC.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<FixItQcDbContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("Default")
        ?? "Host=localhost;Port=5432;Database=fixitqc;Username=postgres;Password=postgres";
    opt.UseNpgsql(conn);
});

builder.Services.AddSingleton<IScopeAuthorizer, ScopeAuthorizer>();
builder.Services.AddSingleton<IFileStorage, LocalFileStorage>();
builder.Services.AddSingleton<IPdfReportRenderer, DeterministicPdfRenderer>();
builder.Services.AddSingleton<InspectionWindowService>();
builder.Services.AddSingleton<FuelingWorkflowService>();
builder.Services.AddSingleton<RadioCommsService>();
builder.Services.AddHostedService<IntegrationProcessingWorker>();
builder.Services.AddHostedService<KpiAggregationWorker>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseMiddleware<RequestCorrelationMiddleware>();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapHub<DispatchHub>("/hubs/dispatch");
app.MapHub<CommsHub>("/hubs/comms");
app.MapHub<BulletinHub>("/hubs/bulletins");

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "FixItQC.Api" }));

app.Run();
