using PRN231.Domain.Common;
using System.Text.Json;

namespace PRN231.API.Middlewares;

internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodeHelpers.ExceptionToStatusCode(exception);
        var response = new
        {
            message = exception.Message,
        };
        var jsonResponse = JsonSerializer.Serialize(response);
        await httpContext.Response.WriteAsync(jsonResponse);
    }
}
