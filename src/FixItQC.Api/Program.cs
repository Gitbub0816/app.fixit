using FixItQC.Application.Abstractions;
using FixItQC.Application.Authorization;
using FixItQC.Infrastructure.Diagnostics;
using FixItQC.Infrastructure.Pdf;
using FixItQC.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IScopeAuthorizer, ScopeAuthorizer>();
builder.Services.AddSingleton<IFileStorage, LocalFileStorage>();
builder.Services.AddSingleton<IPdfReportRenderer, DeterministicPdfRenderer>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseMiddleware<RequestCorrelationMiddleware>();
app.UseExceptionHandler();
app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "FixItQC.Api" }));

app.Run();
