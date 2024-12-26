using System.Diagnostics;

namespace GrpcCrudExample.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var path = context.Request.Path;

            // ثبت زمان شروع
            var startTime = DateTime.UtcNow;

            // ادامه پردازش درخواست
            await _next(context);

            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;

            // لاگ‌گذاری اطلاعات
            _logger.LogInformation("Request: {Path} started at {StartTime} responded with status code {StatusCode} in {ElapsedMilliseconds} ms",
                path, startTime, statusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}