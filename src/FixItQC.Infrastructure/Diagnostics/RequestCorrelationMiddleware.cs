namespace FixItQC.Infrastructure.Diagnostics;

public sealed class RequestCorrelationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCorrelationMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString("N");
        context.Response.Headers["X-Correlation-Id"] = correlationId;
        context.Items["CorrelationId"] = correlationId;
        await _next(context);
    }
}
