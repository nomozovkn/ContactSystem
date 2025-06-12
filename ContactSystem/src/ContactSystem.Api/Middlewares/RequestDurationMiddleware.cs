namespace ContactSystem.Api.Middlewares;

public class RequestDurationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestDurationMiddleware> _logger;
    public RequestDurationMiddleware(RequestDelegate next, ILogger<RequestDurationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var stopWatch = System.Diagnostics.Stopwatch.StartNew();

        await _next(context);

        stopWatch.Stop();

        var duration = stopWatch.ElapsedMilliseconds / 1000.0;
        var path = context.Request.Path;
        var statusCode = context.Response.StatusCode;

        Console.WriteLine($"[Console] Request to {path} took {duration} seconds with status code {statusCode}");

        _logger.LogInformation(
            "Request to {Path} took {Duration} seconds with status code {StatusCode}",
            path, duration, statusCode);

    }
}
