using System.Net;
using Grpc.Core;
using Newtonsoft.Json;

namespace GrpcCrudExample.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception has occurred");

            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case RpcException rpcException:
                    code = MapRpcStatusToHttpStatus(rpcException.Status.StatusCode);
                    result = rpcException.Status.Detail;
                    break;
                default:
                    result = "An error occurred. Please try again later.";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = result }));
        }

        private HttpStatusCode MapRpcStatusToHttpStatus(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.NotFound:
                    return HttpStatusCode.NotFound;
                case StatusCode.InvalidArgument:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}